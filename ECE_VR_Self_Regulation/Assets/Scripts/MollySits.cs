using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MollySits : MonoBehaviour
{
    public GameObject molly;
    public Vector3 goal;
    public float speed;

    //KeywordRecognizer recognizer;

    [SerializeField]
    string[] keywords;
    SpeechToText speechToText;

    // Start is called before the first frame update
    void Start()
    {
        //Change Molly
        foreach (Transform item in molly.transform)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }

        //Create Recognizer
        speechToText = new SpeechToText();
        speechToText.Start();

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
