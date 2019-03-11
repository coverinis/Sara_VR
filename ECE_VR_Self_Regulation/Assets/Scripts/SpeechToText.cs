using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToText
{
    private DictationRecognizer dictationRecognizer;
    private StringBuilder stringBuilder;

    public DictationRecognizer DictationRecognizer
    {
        get { return dictationRecognizer; }
    }
    public string Transcript
    {
        get { return stringBuilder.ToString(); }
    }

    public delegate void FindKeywords(string text);

    public SpeechToText()
    {
        stringBuilder = new StringBuilder();
        BuildDictation();
    }

    public SpeechToText(FindKeywords function)
    {
        stringBuilder = new StringBuilder();
        BuildDictation(function);
    }

    public void Start()
    {
        dictationRecognizer.Start();
    }

    public void Stop()
    {
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
    }

    void BuildDictation()
    {
        //Create Recognizer
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.InitialSilenceTimeoutSeconds = 86400;

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            stringBuilder.Append(text);
            File.AppendAllText(".//Assets//Transcript.txt", text);
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

    void BuildDictation(FindKeywords function)
    {
        //Create Recognizer
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.InitialSilenceTimeoutSeconds = 86400;


        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            stringBuilder.Append(text);
            File.AppendAllText(".//Assets//Transcript.txt", text+".");
            function(text);
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
                BuildDictation(function);
                dictationRecognizer.Start();
            }
            else
            {
                Debug.Log("Restart Dicatation");
                dictationRecognizer.Dispose();
                BuildDictation(function);
                dictationRecognizer.Start();
            }

        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };
    }
}
