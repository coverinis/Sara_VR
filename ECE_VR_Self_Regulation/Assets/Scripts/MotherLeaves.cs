using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherLeaves : MonoBehaviour
{

    public GameObject mother;
    public Vector3 goal;
    public float speed;

    Animator animatorMother;
    AudioSource audioData;
    bool firstUpdate;

    // Start is called before the first frame update
    void Start()
    {
        animatorMother = mother.GetComponent<Animator>();
        audioData = mother.GetComponent<AudioSource>();
        audioData.Play();
        firstUpdate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioData.isPlaying)
        {
            if (firstUpdate)
            {
                mother.transform.Rotate(0, 180, 0);
                animatorMother.SetBool("Idling", false);
                firstUpdate = false;
            }
            float step = speed * Time.deltaTime;
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
}
