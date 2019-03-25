using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuckInMolly : MonoBehaviour
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
            GameState.finishedTuckInMolly = true;
        }
        if (GameState.finishedTuckInMolly)
        {
            enabled = false;
            GetComponent<GetDownToMolly>().enabled = false;
            GetComponent<Empathize>().enabled = false;
            GetComponent<MollyStartsToWiggle>().enabled = true;
        }
    }
}
