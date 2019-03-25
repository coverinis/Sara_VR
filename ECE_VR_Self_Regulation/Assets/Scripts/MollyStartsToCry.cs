using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MollyStartsToCry : MonoBehaviour
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
            GameState.finishedMollyStartsToCry = true;
        }
        if (GameState.finishedMollyStartsToCry)
        {
            enabled = false;
            GetComponent<GetDownToMolly>().enabled = true;
            GetComponent<Empathize>().enabled = true;
            GetComponent<TuckInMolly>().enabled = true;
        }
    }
}
