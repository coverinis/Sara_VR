using MyCouch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToText
{
    //Private
    private DictationRecognizer dictationRecognizer;
    private StringBuilder stringBuilder;

    //Public
    public delegate void FindKeywords(string text);
    public DictationRecognizer DictationRecognizer
    {
        get { return dictationRecognizer; }
    }
    public string Transcript
    {
        get { return stringBuilder.ToString(); }
    }
    public FindKeywords FindKeywordFunction { get; set; }

    //Constructor
    public SpeechToText()
    {
        stringBuilder = new StringBuilder();
        FindKeywordFunction = (text) => { Debug.Log(text); };
        BuildDictation();
    }

    //Functions
    public void Start()
    {
        dictationRecognizer.Start();
    }

    public void Stop()
    {
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
    }

    public void WriteToStorage(string text)
    {
        var credentials = string.Format("{0}:{1}", "iongenetwoughterestimain", "afe6f0c803f5adcf14c3e82ee3690cb222fc1f07");
        var url = string.Format("https://{0}@c86ec1db-aece-4f27-b8c7-9cae5a827ef5-bluemix.cloudant.com/", credentials);

        using (var client = new MyCouchClient(url, "ecesr"))
        {
            //POST with server generated id
            var test = client.Documents.PostAsync($"{{\"date\":\"{DateTime.Now.ToUniversalTime()}\",\"text\":\"{text}\"}}").Result;
            Debug.Log(test.Reason);
        }
    }

    public void WriteToStorage()
    {
        var credentials = string.Format("{0}:{1}", "sferuspecingingstruithem", "fc655e24802ce39e335b96e4697fd4f48bf4be28");
        var url = string.Format("https://{0}@c32ee9af-4fd5-4306-9556-49996adca89b-bluemix.cloudant.com/", credentials);

        using (var client = new MyCouchClient(url, "ecesr"))
        {
            //POST with server generated id
            var test = client.Documents.PostAsync($"{{\"date\":\"{DateTime.Now.ToUniversalTime()}\",\"text\":\"{Transcript}\"}}").Result;
            Debug.Log(test.Reason);
        }
    }

    void BuildDictation()
    {
        //Create Recognizer
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.InitialSilenceTimeoutSeconds = 86400;


        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            stringBuilder.Append(text + ".");
            Debug.Log(Transcript);//WriteToStorage(Transcript);
            FindKeywordFunction(text);
        };

        dictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
        };

        dictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
            {
                Debug.LogErrorFormat("{0}", completionCause);
                Debug.Log("Restart Dicatation");
                dictationRecognizer.Dispose();
                BuildDictation();
                dictationRecognizer.Start();
            }
            else
            {
                Debug.Log("Restart Dicatation");
                dictationRecognizer.Dispose();
                BuildDictation();
                dictationRecognizer.Start();
            }

        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };
    }
}
