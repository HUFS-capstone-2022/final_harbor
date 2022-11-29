using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGrayScale : MonoBehaviour
{
    // Value for Material
    private Material _material;

    // Lerp Value
    [Range(0.0f, 1.0f)]
    public float grayScaleLerpVal = 0.0f;

    // Awake()
    void Awake()
    {
        // Add Shader in [Edit] - [Project Setting] - [Graphics] - [Always included shaders] for use this
        _material = new Material(Shader.Find("Hidden/GrayScale"));
    }

    // Update is called once per frame
    void Update()
    {
        if (NormalManager.Instance.makeGrayScale)
        {
            StartCoroutine(grayCoroutine());
            Debug.Log("gray Coroutine called!!");
            NormalManager.Instance.makeGrayScale = false;
        }
    }

    // This makes screen change
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (grayScaleLerpVal == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        _material.SetFloat("_GrayScale", grayScaleLerpVal);
        Graphics.Blit(source, destination, _material);
    }

    // Coroutine that makes screen GrayScale
    IEnumerator grayCoroutine()
    {
        while (grayScaleLerpVal < 1.0f)
        {
            grayScaleLerpVal += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        NormalManager.Instance.getDB = true;
    }
}
