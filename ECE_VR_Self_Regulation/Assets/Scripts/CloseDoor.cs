using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseDoor : MonoBehaviour
{
    public Animator animator;
    public OpenDoor openDoor; 

    private void Start()
    {
        openDoor.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            animator.SetTrigger("Close");
            this.gameObject.SetActive(false);
            GameState.speechToText.WriteToStorage();
            Application.Quit();
        }
    }
}
