using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator door;

    // Start is called before the first frame update
    void Start()
    {
        door.SetTrigger("Open");
    }

}
