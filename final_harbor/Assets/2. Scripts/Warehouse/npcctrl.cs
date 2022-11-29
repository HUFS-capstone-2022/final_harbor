/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcctrl : MonoBehaviour
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
    Transform target;
    float enemyMoveSpeed = 4f;
    public float movespeed = 5.0f;
    public bool isDie = false;
    private void Start()
    {
        npcTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        //InvokeRepeating("UpdateTarget", 0f, 0.25f);

        StartCoroutine(npcState());
    }
    IEnumerator npcState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            float distance = Vector3.Distance(playerTr.position, npcTr.position);

            if (FakeDoor.touchedDoor == true)
            {
                state = State.Standup;
                anim.SetTrigger("Moving");
            }
            else
            {
                state = State.Laying;
            }
            yield return null;
        }

    }
   
}
*/