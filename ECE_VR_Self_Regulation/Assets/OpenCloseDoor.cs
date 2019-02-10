using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OpenCloseDoor : MonoBehaviour
{

    public Animator Animator { get => animator; }
    public string Open { get => open; }
    public string Close { get => close; }
    public int time;

    private Animator animator;
    private string open;
    private string close;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        open = "Open";
        close = "Close";
        animator.SetTrigger(Open);
        Invoke("trigerClose", time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void trigerClose()
    {
        animator.SetTrigger(Close);
    }
}

[CustomEditor(typeof(OpenCloseDoor))]
public class OpenCloseDoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        OpenCloseDoor openCloseDoor = (OpenCloseDoor)target;
        openCloseDoor.time = EditorGUILayout.IntField("Time", openCloseDoor.time);

        this.name = "Open Close";
    }
}