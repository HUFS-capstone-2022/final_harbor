using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoor : MonoBehaviour
{
    public float forceAmount = 1000f;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Out")
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * forceAmount, ForceMode.Acceleration);
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Out")
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * forceAmount, ForceMode.Acceleration);
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
