using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script execution order --> This script have to execute faster than other scripts
// If number bigger, execute later!
[DefaultExecutionOrder(0)]
public class TrueManager : MonoBehaviour
{
// SingleTon
    // Make one static instance
    private static TrueManager _instance;
    // Make property that help other scripts using instance
    public static TrueManager Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

// Values

// 1. Values for Making Units Move
    // If fade Out
    public bool FadeOut = false;

    // 4. Make screen Fade In after get DB data
    public bool makeFadeOut = false;

    public bool makePlayerMoving = false;

    // Awake()
    private void Awake()
    {
        // If do not have Instance, this object will be Instance
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        // If have Instance, Instance have to be only one. So, destroy this game object.
        Destroy(gameObject);
    }
}
