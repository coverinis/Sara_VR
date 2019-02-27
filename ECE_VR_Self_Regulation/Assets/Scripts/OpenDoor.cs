using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator door;
    public GameObject showTrigger;

    static public bool bedroomScene;

    // Start is called before the first frame update
    void Start()
    {
        bedroomScene = false;
    }

    private void Update()
    {
        if (bedroomScene)
        {
            door.SetTrigger("Open");
            showTrigger.SetActive(true);
        }
    }

}
