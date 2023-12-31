using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script lets the ai simply follow the player around.
public class AIFollow : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    Animator anim;

    float distance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, target.transform.position); //Distance between the NPC and the target.

        if (distance <= 120)
        {
            agent.destination = target.position;    //The NPC goes to the set location.
        }

        //If the NPC reaches its destination, it's velocity will be 0 and the idle animation will play.
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.velocity = Vector3.zero;
            anim.SetInteger("State", 0);
        }
        else //If the NPC is moving...
        {
            anim.SetInteger("State", 1);
        }
    }
}