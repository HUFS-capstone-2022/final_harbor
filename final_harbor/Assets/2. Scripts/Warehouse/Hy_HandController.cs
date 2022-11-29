using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(ActionBasedController))]
public class Hy_HandController : MonoBehaviour
{
    ActionBasedController controller;
    public Hy_Hand hand;
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    void Update()
    {
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
