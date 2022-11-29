using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeOutline : MonoBehaviour
{
    Material outline;

    Renderer renderers;
    List<Material> materialList = new List<Material>();
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Out")
        {
            renderers = this.GetComponent<Renderer>();
            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outline);

            renderers.materials = materialList.ToArray();
        }
    }
    void OnTriggerStay(Collider other)
    {
        Renderer renderer = this.GetComponent<Renderer>();
        materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);
        materialList.Remove(outline);

        renderer.materials = materialList.ToArray();
    }
    void OnTriggerExit(Collider other)
    {
        Renderer renderer = this.GetComponent<Renderer>();
        materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);

        materialList.Remove(outline);

        renderer.materials = materialList.ToArray();
    }
    void Start()
    {
        outline = new Material(Shader.Find("Valve/VR/Silhouette"));
    }
}
