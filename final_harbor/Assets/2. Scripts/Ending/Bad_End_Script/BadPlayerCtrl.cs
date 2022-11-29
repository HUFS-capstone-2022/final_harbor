using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPlayerCtrl : MonoBehaviour
{
    // 0. Must need values
    private Transform playerTr;

    // 1. Values for Smooth Damp
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 2.0f;
    private Vector3 destination = new Vector3(0.0f, 0.0f, 1.0f);
    float distance = 0.0f;
    private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GetComponent<Transform>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make player gameObject move.
        if (BadManager.Instance.playerMove)
        {
            playerTr.position = Vector3.SmoothDamp(playerTr.position, destination, ref velocity, smoothTime);
            distance = Vector3.Distance(playerTr.position, destination);
            
            // If Player is about arrived
            if (distance < 0.15f)
            {
                BadManager.Instance.playerMove = false;
                Debug.Log("Player is arrived!");
                BadManager.Instance.makeUnitMove = true;
            }
        }
    }
}
