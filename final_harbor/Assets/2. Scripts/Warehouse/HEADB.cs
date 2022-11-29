using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
using UnityEngine.XR;


public class HEADB : MonoBehaviour
{
    public Transform primary2DAxisTran;
    public PXR_Input.Controller controller;
    private InputDevice currentController;
    private Vector2 axis2D = Vector2.zero;
    private Transform camtr;
    private Camera cam;
    public float radius = 0.3f;
    private Vector3 ScreenCenter;
    LayerMask wallCol;
    // Start is called before the first frame update
    void Start()
    {
        currentController = InputDevices.GetDeviceAtXRNode(controller == PXR_Input.Controller.LeftController
                ? XRNode.LeftHand
                : XRNode.RightHand);
        currentController.TryGetFeatureValue(CommonUsages.primary2DAxis, out axis2D);
        camtr = Camera.main.GetComponent<Transform>();
        
        wallCol = LayerMask.NameToLayer("Block");
        StartCoroutine("calculatecol");
    }

    // Update is called once per frame
    IEnumerator calculatecol()
    {
        while (true)
        {
            int layerMask = ~(1 << wallCol);
            Collider[] colliders = Physics.OverlapSphere(camtr.transform.position, radius, layerMask);
            foreach (Collider col in colliders)
            {
                if (col.name == "PlayerL") continue;
                StartCoroutine("Blocking");
            }
            yield return null;
        }
    }
    IEnumerator Blocking()
    {
        while (true)
        {
            if (currentController.TryGetFeatureValue(CommonUsages.primary2DAxis, out axis2D))
            {
                if (axis2D.y >= 0)
                {
                    Debug.Log("y: "+axis2D.y);
                    var inputVector = new Vector3(0, 0, axis2D.y);
                    //var inputDir = transform.TransformDirection(inputVector);
                    var lookDir = new Vector3(0, camtr.eulerAngles.y, 0);
                    var newDir = Quaternion.Euler(lookDir) * inputVector;
                    //transform.Translate(moveDir * Time.deltaTime);
                    transform.Translate(newDir * Time.deltaTime * 0);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }

    }
}
