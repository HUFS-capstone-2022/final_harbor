using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalCar2 : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Vector3 destination = new Vector3(10.72f, 0.0f, 57.2f);

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NormalManager.Instance.carMoves2)
        {
            navAgent.SetDestination(destination);
            NormalManager.Instance.carMoves2 = false;
        }

        if (Vector3.Distance(this.transform.position, destination) < 0.1f)
        {
            NormalManager.Instance.makeUnitMove = true;
        }
    }
}
