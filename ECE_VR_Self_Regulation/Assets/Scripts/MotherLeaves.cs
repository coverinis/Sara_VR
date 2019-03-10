using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherLeaves : MonoBehaviour
{

    public GameObject mother;
    public Vector3 goal;
    public float speed;

    Animator animatorMother;

    // Start is called before the first frame update
    void Start()
    {
        animatorMother = mother.GetComponent<Animator>();
        mother.transform.Rotate(0, 180, 0);
        animatorMother.SetBool("Idling", false);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        // mother.transform.position = Vector3.MoveTowards(mother.transform.position, goal, step);
        if (mother.transform.position.z <= goal.z)
        {
            GameState.finishedMotherLeaves = true;
            animatorMother.SetBool("Idling", true);
        }
        if (GameState.finishedMotherLeaves)
        {
            enabled = false;
            GetComponent<PrepareFood>().enabled = true;
        }
    }
}
