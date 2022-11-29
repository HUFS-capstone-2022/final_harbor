using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Script execution order --> This script have to execute faster than other scripts
// If number bigger, execute later!
[DefaultExecutionOrder(0)]
public class BadManager : MonoBehaviour
{

// SingleTon
    // Make one static instance
    private static BadManager _instance;
    // Make property that help other scripts using instance
    public static BadManager Instance
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

    // 0. Must need values
    // Value for caching player's transform component
    private Transform playerTr;
    public bool playerMove = false;

// 1. Values for Making Units Move Functions
// Make Class List for ccontrol every units in "Bad_End_Scene"
    public List<BadUnit> Units = new List<BadUnit>();
    // Bool for make units move.
    public bool makeUnitMove = false;
    // Base distance of units and player
    public float RadiusAroundTarget = 1.5f;
    // Arrived unit number
    public int arrivedUnitNum = 0;

// 2. Make screen grayScale when 10 units are arrived
    // Value for make screen GrayScale
    public bool makeGrayScale = false;
    // Check for makeGrayScale called
    public bool calledGrayScale = false;

// 3. Get DB data
    public bool getDB = false;


// 4. Make screen Fade In after get DB data
    public bool makeFadeOut = false;



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


// Start
    void Start()
    {
        // Assign Components of player
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        Debug.Log("start value of PlayerPrefs_ending = " + PlayerPrefs.GetInt("ending"));
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Make Units Move
        if (makeUnitMove)
        {
            for (int i = 0; i < Units.Count; i++)
            {
                Units[i].SetNavDestination(new Vector3(
                playerTr.position.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / Units.Count),
                0,
                playerTr.position.z + RadiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i / Units.Count)
                ));

                Units[i].NavAgentWork(true);
                Units[i].moving = true;
                Units[i].WalkAnim(true);
            }
            Debug.Log("Ordered to units move");
            makeUnitMove = false;
        }

        // 2. Make screen grayScale
        if (arrivedUnitNum == 6 && !calledGrayScale)
        {
            Debug.Log("Ordered to make screen GrayScale");
            makeGrayScale = true;
            calledGrayScale = true;
        }

        // 3. get DB data
        if (getDB)
        {
            Debug.Log("getting DB by UI");
            // makeFadeOut = true;
            PlayerPrefs.SetInt("ending", 1);
            Debug.Log("PlayerPrefs_ending = " + PlayerPrefs.GetInt("ending"));
            getDB = false;
        }
    }
}
