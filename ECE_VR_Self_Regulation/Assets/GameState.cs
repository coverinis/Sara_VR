using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static bool finishedWalkIn;
    public static bool finishedMollySits;

    // Start is called before the first frame update
    void Start()
    {
        finishedWalkIn = false;
        finishedMollySits = false;
    }
}
