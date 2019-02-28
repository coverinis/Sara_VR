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

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform item in molly.transform)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }
        //recognizer = new KeywordRecognizer(keywords);
        //recognizer.OnPhraseRecognized += OnPhraseRecognized;
        //recognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
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
            //GetComponent<MollySits>().enabled = true;
        }
    }
}
