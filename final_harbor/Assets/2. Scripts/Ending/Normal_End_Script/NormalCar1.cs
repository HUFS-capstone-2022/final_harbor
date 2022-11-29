using UnityEngine;
using UnityEngine.AI;

public class NormalCar1 : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Vector3 destination = new Vector3(-9.57f, 0.0f, 55.63f);


    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NormalManager.Instance.carMoves)
        {
            navAgent.SetDestination(destination);

            NormalManager.Instance.carMoves = false;
        }
    }
}
