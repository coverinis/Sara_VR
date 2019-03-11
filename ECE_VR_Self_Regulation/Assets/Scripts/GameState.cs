using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static bool finishedWalkIn;
    public static bool finishedMollySits;
    public static bool finishedMotherLeaves;
    public static bool finishedPrepareFood;
    public static bool finishedDetectFood;
    public static bool finishedMoveToRoom;

    // Start is called before the first frame update
    void Start()
    {
        finishedWalkIn = false;
        finishedMollySits = false;
        finishedMotherLeaves = false;
        finishedPrepareFood = false;
        finishedDetectFood = false;
        finishedMoveToRoom = false;
    }
}
