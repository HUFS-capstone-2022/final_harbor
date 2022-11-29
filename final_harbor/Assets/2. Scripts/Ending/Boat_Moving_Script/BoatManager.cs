using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class BoatManager : MonoBehaviour
{
// SingleTon
    // Make one static instance
    private static BoatManager _instance;
    // Make property that help other scripts using instance
    public static BoatManager Instance
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

    // 3. Get DB data
    public bool getDB = false;

    // 4. Make screen Fade In after get DB data
    public bool makeFadeOut = false;

    public bool makePlayerMove = false;

    public bool makeGrayScale = false;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // 3. get DB data
        if (getDB)
        {
            Debug.Log("getting DB by UI");
            // makeFadeOut = true;
            // Debug.Log("got data and Ordered to make screen FadeIn");
            getDB = false;
        }
    }
}
