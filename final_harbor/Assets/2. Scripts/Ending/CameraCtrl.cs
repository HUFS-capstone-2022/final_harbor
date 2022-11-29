using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    // Value for Transform cache
    private Transform tr;

    // Set move and turn speed
    public float moveSpeed = 8.0f;
    public float turnXSpeed = 100.0f;

    // Start
    void Start()
    {
        // Get this gameObject(MainCamera)'s Transform Component
        tr = GetComponent<Transform>();
    }

    // Update
    void Update()
    {
        // get keyboard input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // get mouse input
        float rX = Input.GetAxis("Mouse X");

        // Set direction for moving by v and h
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // Move gameObject(MainCamera) toward direction
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        // Turn gameObject(MainCamera) with Rotate
        tr.Rotate(0, turnXSpeed * Time.deltaTime * rX, 0);
    }
}