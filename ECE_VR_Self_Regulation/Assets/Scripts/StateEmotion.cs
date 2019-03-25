using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEmotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (false)
        {
            GameState.finishedStateEmotion = true;
        }
        if (GameState.finishedStateEmotion)
        {
            enabled = false;
            GetComponent<ApproachMolly>().enabled = true;
            GetComponent<GetDownToLevel>().enabled = true;
            GetComponent<TouchShoulders>().enabled = true;
            GetComponent<AskForHug>().enabled = true;
        }
    }
}
