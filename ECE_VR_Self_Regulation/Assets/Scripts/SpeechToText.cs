﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToText
{
    private DictationRecognizer dictationRecognizer;

    public SpeechToText()
    {
        BuildDictation();
    }

    public SpeechToText(Func<string,bool> function)
    {
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

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
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
                dictationRecognizer.Stop();
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

    void BuildDictation(Func<string, bool> function)
    {
        //Create Recognizer
        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
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
                dictationRecognizer.Stop();
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
