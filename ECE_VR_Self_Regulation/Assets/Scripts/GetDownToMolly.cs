using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDownToMolly : MonoBehaviour
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
            GameState.finishedGetDownToMolly = true;
        }
        if (GameState.finishedGetDownToMolly)
        {
            enabled = false;
        }
    }
}
