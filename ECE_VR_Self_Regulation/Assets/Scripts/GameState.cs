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
    public static bool finishedAskMollyToLieDown;
    public static bool finishedMollyStartsToCry;
    public static bool finishedGetDownToMolly;
    public static bool finishedEmpathize;
    public static bool finishedTuckInMolly;
    public static bool finishedMollyStartsToWiggle;


    public static SpeechToText speechToText;

    // Start is called before the first frame update
    void Start()
    {
        finishedWalkIn = false;
        finishedMollySits = false;
        finishedMotherLeaves = false;
        finishedPrepareFood = false;
        finishedDetectFood = false;
        finishedMoveToRoom = false;
        finishedAskMollyToLieDown = false;
        finishedMollyStartsToCry = false;
        finishedGetDownToMolly = false;
        finishedEmpathize = false;
        finishedTuckInMolly = false;
        finishedMollyStartsToWiggle = false;


        speechToText = new SpeechToText();
        speechToText.Start();
    }
}
