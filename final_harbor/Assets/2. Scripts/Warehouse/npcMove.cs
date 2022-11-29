using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMove : MonoBehaviour
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
    private Animator anim;
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    float enemyMoveSpeed = 4f;
    public float movespeed = 5.0f;
    public bool isDie = false;
    private bool isStill = false;
    private void Start()
    {
        npcTr = GameObject.FindWithTag("npc").GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
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

            if (FakeDoor.touchedDoor == true && !isStill)
            {
                anim.SetTrigger("Moving");
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                state = State.Laying;
            }
            if (distance <= traceDist)
            {
                isStill = true;
                state = State.Run;
                anim.SetTrigger("TrigTrace");
            }
            if (distance <= attackDist)
            {
                state = State.Attack;
                anim.SetTrigger("TrigAttack");
            }
        } 
    }
        IEnumerator npcAction()
        {
            while (!isDie)
            {
                switch (state)
                {
                    case State.Run:
                         Vector3 dir = playerTr.position - transform.position;
                         transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);
                        break;
                    case State.Attack:
                        isDie = true;
                        break;
                }
                yield return new WaitForSeconds(0.3f);
            }
        }
        /*private void UpdateTarget()
         {
             while (!isDie)
             {
                 Collider[] cols = Physics.OverlapSphere(transform.position, 20f, 1 << 8);
                 if (cols.Length > 0)
                 {
                     for (int i = 0; i < cols.Length; i++)
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
         }
        void Update()
         {
             float distance = Vector3.Distance(playerTr.position, npcTr.position);
             if (distance <= 3f)
             {
                 state = State.Attack;
                 anim.SetBool(hashAttack, true);
                 isDie = true;
             }
             if (target != null && FakeDoor.touchedDoor==true &&!isDie)
             {
                 state = State.Wakeup;
                 state = State.Standup;
                 Vector3 dir = target.position - transform.position;
                 transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);
                 state = State.Run;
                 anim.SetBool(hashTrace, true);       
             }
             else
             {
                 state = State.Laying;
                 anim.SetBool(hashTrace, false);
             }

         }*/
    
}