using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFood : MonoBehaviour
{
    public Rigidbody food;
    public float waitTime;
    public OpenDoor openDoor;
    public MoveToRoom moveToRoom;

    private float collisionTime;

    // Start is called before the first frame update
    void Start()
    {
        collisionTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Food"))
        {
            collisionTime += Time.deltaTime;
            if (collisionTime >= waitTime)
            {
                food.AddForce(-8, 8, 6);
                GameState.finishedDetectFood = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameState.finishedDetectFood)
        {
            gameObject.SetActive(false);
            openDoor.enabled = true;
            moveToRoom.enabled = true;
        }
    }
}
