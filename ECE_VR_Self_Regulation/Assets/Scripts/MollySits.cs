using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    Regex regex;
    string pattern;

    // Start is called before the first frame update
    void Start()
    {
        //Change Molly
        foreach (Transform item in molly.transform)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }

        //Create pattern
        StringBuilder stringBuilder = new StringBuilder("");
        for (int i = 0; i < keywords.Length; i++)
        {
            if (i != keywords.Length - 1)
            {
                stringBuilder.Append($"\\b{keywords[i]}|");
            }
            else
                stringBuilder.Append($"\\b{keywords[i]}");
        }

        regex = new Regex(stringBuilder.ToString(),RegexOptions.IgnoreCase);
        bool test = regex.IsMatch("How is Molly Today");

        //Create Recognizer
        speechToText = new SpeechToText(FindKeywords);
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

    /// <summary>
    /// Log when a Keyword was found
    /// </summary>
    /// <param name="text">text to process</param>
    void FindKeywords(string text)
    {
        if (regex.IsMatch(text))
        {
            Debug.Log("Found keyword");
        }
    }
}
