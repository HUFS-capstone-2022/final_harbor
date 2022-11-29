using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    public GameObject prefab;
    GameObject equipKey;
    private bool isInstance = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key" && isInstance == false )
        {
            Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            GrabHandPosekey g = new GrabHandPosekey();
            g.stopHandPose();
            Destroy(other.gameObject);
            Debug.Log("key");
            isInstance = true;
        }
    }
}
