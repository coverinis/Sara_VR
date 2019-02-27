using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIn : MonoBehaviour
{
    public GameObject mother;
    public GameObject molly;
    public Vector2 moveTo;
    public float speed;


    private Vector3 goalMother;
    private Vector3 goalMolly;
    private Animator animatorMother;

    // Start is called before the first frame update
    void Start()
    {
        goalMother = new Vector3(mother.transform.position.x + moveTo.x, mother.transform.position.y, mother.transform.position.z + moveTo.y);
        goalMolly = new Vector3(molly.transform.position.x + moveTo.x, molly.transform.position.y, molly.transform.position.z + moveTo.y);
        animatorMother = mother.GetComponent<Animator>();
        animatorMother.SetBool("Idling", false);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        mother.transform.position = Vector3.MoveTowards(mother.transform.position, goalMother, step);
        molly.transform.position = Vector3.MoveTowards(molly.transform.position, goalMolly, step);
        if (mother.transform.position.Equals(goalMother))
        {
            animatorMother.SetBool("Idling", true);
        }
    }
}
