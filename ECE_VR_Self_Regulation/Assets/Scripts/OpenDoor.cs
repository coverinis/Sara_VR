using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator door;
    public GameObject showTrigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        door.SetTrigger("Open");
        showTrigger.SetActive(true);
    }

}
