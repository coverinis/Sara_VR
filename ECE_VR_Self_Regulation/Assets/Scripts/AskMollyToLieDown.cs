using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskMollyToLieDown : MonoBehaviour
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
            GameState.finishedAskMollyToLieDown = true;
        }
        if (GameState.finishedAskMollyToLieDown)
        {
            enabled = false;
            GetComponent<MollyStartsToCry>().enabled = true;
        }
    }
}
