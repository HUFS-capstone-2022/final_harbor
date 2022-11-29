using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fall : MonoBehaviour
{
    float forceGravity = 1000f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * forceGravity);

        }
    }

}
