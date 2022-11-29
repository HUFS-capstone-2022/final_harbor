using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
public class npcMovetest : MonoBehaviour
{
    public enum State
    {
        Laying,
        Wakeup,
        Standup,
        Run,
        Attack
    }
    public State state = State.Laying;
    public float traceDist = 30.0f;
    public float attackDist = 2.0f;

    private Transform npcTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    Transform target;
    //float enemyMoveSpeed = 2f;
    //public float movespeed = 5.0f;
    public bool isDie = false;
    private void Start()
    {
        npcTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //animator = GetComponent<animator>();
        //InvokeRepeating("UpdateTarget", 0f, 0.25f);

        StartCoroutine(npcState());
        StartCoroutine(npcAction());
    }
    IEnumerator npcState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            float distance = Vector3.Distance(playerTr.position, npcTr.position);

            if (FakeDoor.touchedDoor == true)
            {
                Debug.Log("touchDoorTrue");
                state = State.Standup;
                if (distance <= traceDist)
                    state = State.Run;
                else if (distance <=attackDist)
                    state = State.Attack;
            }
            else
            {
                state = State.Laying;
            }

            
        }
    }
    IEnumerator npcAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.Laying:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;
                case State.Run:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;

                    anim.SetBool(hashTrace, true);

                    //anim.SetBool(hashAttack, false);
                    break;
                case State.Attack:
                    anim.SetBool(hashAttack, true);
                    isDie = true;
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
   /* private void UpdateTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 10f, 1 << 8);
        if(cols.Length > 0)
        {
            for(int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Player")
                {
                    target = cols[i].gameObject.transform;
                }
            }
        }
        else
        {
            target = null;
        }
    }
    void Update()
    {
        if(target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);
        }
    }*/
}
