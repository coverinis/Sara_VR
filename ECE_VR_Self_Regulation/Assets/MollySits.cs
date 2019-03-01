using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MollySits : MonoBehaviour
{
    public GameObject molly;
    public Vector3 goal;
    public float speed;

    //KeywordRecognizer recognizer;

    [SerializeField]
    string[] keywords;
    DictationRecognizer m_DictationRecognizer;

    // Start is called before the first frame update
    void Start()
    {
        //Change Molly
        foreach (Transform item in molly.transform)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }

        //Create Recognizer
        BuildDictation();

        m_DictationRecognizer.Start();

    }

    void BuildDictation()
    {
        //Create Recognizer
        m_DictationRecognizer = new DictationRecognizer();

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
            {
                Debug.LogErrorFormat("{0}", completionCause);
                Debug.Log("Restart Dicatation");
                m_DictationRecognizer.Stop();
                m_DictationRecognizer.Dispose();
                BuildDictation();
                m_DictationRecognizer.Start();
            }

        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        molly.transform.position = Vector3.MoveTowards(molly.transform.position, goal, step);
        if (molly.transform.position.Equals(goal) && false)
        {
            GameState.finishedMollySits = true;
        }
        if (GameState.finishedMollySits)
        {
            enabled = false;
        }
    }
}
