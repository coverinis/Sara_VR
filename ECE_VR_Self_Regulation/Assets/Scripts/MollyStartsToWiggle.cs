using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MollyStartsToWiggle : MonoBehaviour
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
            GameState.finishedMollyStartsToWiggle = true;
        }
        if (GameState.finishedMollyStartsToWiggle)
        {
            enabled = false;
            GetComponent<StateEmotion>().enabled = true;
        }
    }
}
