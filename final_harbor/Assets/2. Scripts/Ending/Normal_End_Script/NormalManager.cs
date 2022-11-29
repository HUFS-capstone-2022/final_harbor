using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class NormalManager : MonoBehaviour
{
// SingleTon
    // Make one static instance
    private static NormalManager _instance;
    // Make property that help other scripts using instance
    public static NormalManager Instance
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

// 1. Values for Making Units Move Functions
    // make player and police car move
    public bool makeStart = false;
    public bool carMoves = false;
    public bool carMoves2 = false;

    // to make units move
    public bool makeUnitMove = false;
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
    }

    // Update is called once per frame
    void Update()
    {
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
            PlayerPrefs.SetInt("ending", 2);
            // makeFadeOut = true;
            Debug.Log("PlayerPrefs_ending = " + PlayerPrefs.GetInt("ending"));
            getDB = false;
        }
    }
}
