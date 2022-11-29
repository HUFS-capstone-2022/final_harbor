using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect_player : MonoBehaviour
{
    MeshRenderer mesh;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            mat.color = Color.red;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            mat.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
