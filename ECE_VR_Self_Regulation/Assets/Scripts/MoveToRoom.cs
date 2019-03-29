using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRoom : MonoBehaviour
{
    public GameObject molly;
    public List<Vector3> moveTo;
    public List<Vector3> rotations;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GameState.speechToText.WriteToStorage();
        //Change Molly
        foreach (Transform item in molly.transform)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }

        molly.transform.Rotate(rotations[0]);
        rotations.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (!molly.transform.position.Equals(moveTo[0]))
        {
            molly.transform.position = Vector3.MoveTowards(molly.transform.position, moveTo[0], step);
        }
        else
        {
            moveTo.RemoveAt(0);
            if (!rotations.Count.Equals(0))
            {
                molly.transform.Rotate(rotations[0]);
                rotations.RemoveAt(0);
            }
        }
           
        if (moveTo.Count.Equals(0))
        {
            GameState.finishedMoveToRoom = true;
        }
        if (GameState.finishedMoveToRoom)
        {
            if (!rotations.Count.Equals(0))
            {
                molly.transform.Rotate(rotations[0]);
                rotations.RemoveAt(0);
            }
            enabled = false;
            GetComponent<AskMollyToLieDown>().enabled = true;
        }
    }
}
