using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BoatMoving : MonoBehaviour
{
    private Transform boatTr;

// 1. Values for Smooth Damp
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 2.0f;
    private Vector3 destination = new Vector3(1000.0f, 13.14f, -48.8f);

    // Start is called before the first frame update
    void Start()
    {
        boatTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make player gameObject move.
        if (BoatManager.Instance.makePlayerMove)
        {
            // Slerp
            Vector3 direction = destination - boatTr.position;
            Quaternion rot = Quaternion.LookRotation(direction);

            boatTr.rotation = Quaternion.Slerp(boatTr.rotation, rot, Time.deltaTime * 0.8f);
            boatTr.position = Vector3.Lerp(boatTr.position, destination, Time.deltaTime*0.1f);
          
            // If Player is about arrived
            if (boatTr.position.x > 700.0f)
            {
                BoatManager.Instance.makePlayerMove = false;
                Debug.Log("Player is arrived!");
                BoatManager.Instance.makeGrayScale = true;
            }
        }
    }


}
