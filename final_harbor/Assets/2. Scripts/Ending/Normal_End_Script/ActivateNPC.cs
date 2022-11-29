using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NormalManager.Instance.makeUnitMove)
        {
            GameObject.Find("forNPC").transform.Find("NPCs").gameObject.SetActive(true);
            NormalManager.Instance.makeUnitMove = false;
        }
    }
}
