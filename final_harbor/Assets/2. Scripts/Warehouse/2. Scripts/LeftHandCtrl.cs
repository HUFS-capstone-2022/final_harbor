using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
//using System.Runtime.InteropServices;

namespace Unity.XR.PXR
{
    public class LeftHandCtrl : MonoBehaviour
    {
        public Transform primary2DAxisTran;
        private bool JoystickValue;
        private PXR_Input.Controller controller;
        private InputDevice currentController;
        private Vector2 axis2D = Vector2.zero;
        private Vector3 originalJoystick;

        public int speedForward = 12;
        public int speedSide = 6;

        private Transform tr;
        private float dirX = 0;
        private float dirZ = 0;

        void Start()
        {
            currentController = InputDevices.GetDeviceAtXRNode(controller == PXR_Input.Controller.LeftController ? XRNode.LeftHand : XRNode.RightHand);
            tr = GetComponent<Transform>();
        }

        void Update()
        {
            MovePlayer();
           
            //if (currentController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out JoystickValue) && JoystickValue) {
            //Debug.Log("is pressed");
            /*PXR_HandTracking NHandTracking = PXR_HandTracking();
            NHandTracking.GetJointLocations(hand, jointLocations);*/
        }
        void MovePlayer()
        {
            dirX = 0;
            dirZ = 0;
            if (currentController.TryGetFeatureValue(CommonUsages.primary2DAxis, out axis2D))
            {
                float mx = Mathf.Clamp(axis2D.x * 10f, -10f, 10f);
                float mz = Mathf.Clamp(axis2D.y * 10f, -10f, 10f);
                if (mx > mz) {
                    if (axis2D.x > 0)
                    {
                        dirX = +1;
                    }
                }
                else
                {
                    if (axis2D.y > 0)
                    {
                        dirZ = +1;
                    }
                    else
                    {
                        dirZ = -1;
                    }
                }
            }
            Vector3 moveDir = new Vector3(dirX * speedSide, 0, dirZ * speedForward);
            transform.Translate(moveDir * Time.smoothDeltaTime);
        }
    }
  }