using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script lets the ai flee from a target.
public class AIFlee : MonoBehaviour
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

        if (distance <= 50)
        {
            Vector3 targetDistance = transform.position - target.transform.position; //Vector3 distance between the objects.

            Vector3 newPos = transform.position + targetDistance; //The ai will move to this position

            agent.destination = newPos;    //The NPC goes to the set location. (Away from the target)
        }

        //If the NPC reaches its destination, it's velocity will be 0 and the idle animation will play.
        if (distance >= 140)
        {
            agent.velocity = Vector3.zero;
            
        }
        else //If the NPC is moving...
        {
            anim.SetInteger("State", 1);
        }

        if(agent.velocity == Vector3.zero)
        {
            anim.SetInteger("State", 0);
        }
    }
}
