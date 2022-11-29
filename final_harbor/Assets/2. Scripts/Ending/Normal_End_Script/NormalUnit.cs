using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NormalUnit : MonoBehaviour
{
    // Value for handle cache of component
    private Transform playerTr;
    private Transform unitTr;
    private NavMeshAgent navAgent;
    private Animator anim;

    private Vector3 destination = new Vector3(4.61f, 0.0f, 53.44f);

    // Check, if this object wants to attack
    public bool isAttack = false;
    public bool moving = true;

    // Awake()
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(destination);
        anim = GetComponent<Animator>();

        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        unitTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // change unit's rotation to player
        Quaternion lookRotation = Quaternion.LookRotation((playerTr.position - unitTr.position).normalized);
        unitTr.rotation = Quaternion.Slerp(unitTr.rotation, lookRotation, 4.0f * Time.deltaTime);

        if (moving)
        {
            // Get distance of enemy and player
            float distance = Vector3.Distance(destination, unitTr.position);

            // If unit is close enough to player
            if (distance <= 0.1f)
            {
                // Stop NavMeshAgent
                navAgent.isStopped = true;

                // If unit is arrived, count the unit number
                NormalManager.Instance.arrivedUnitNum++;

                Debug.Log("unit is arrived");

                anim.SetBool("Shooting", true);

                moving = false;
            }
        }

    }
}
