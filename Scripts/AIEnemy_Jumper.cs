using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy_Jumper : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    public Transform defaultPos;
    public GameObject attackCollider;
    public GameObject healthPickup;
    public GameObject dirtParticle;
    Animator anim;
    float distance;
    bool triggered = false;
    bool damaged;
    bool damagedelay = false;

    public bool jumping;

    bool playerDead;
    bool dead = false;

    //For the pickup system
    bool pickupInstantiated = false;
    Vector3 pickupLocation;
    float pickupY;

    //Different trigger and lose distances
    float defTriggerdistance = 250;
    float defLosedistance = 350;
    float shotTriggerdistance;
    float shotLosedistance;
    bool shotsfired;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("Appear");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        distance = Vector3.Distance(this.transform.position, player.transform.position); //Distance between the enemy and the target.
        attackCollider.SetActive(false);
        playerDead = player.GetComponent<PlayerStats>().isDead;
        dead = GetComponentInChildren<EnemyStats>().isDead;
        pickupY = gameObject.transform.position.y + 3.33f;
        pickupLocation = new Vector3(this.transform.position.x, pickupY, this.transform.position.z);
        damaged = GetComponentInChildren<EnemyStats>().damaged;
        shotsfired = GetComponentInChildren<EnemyStats>().shotsfired;

        //If the enemy gets shot from a distance, they will still start pursuing their target.
        shotTriggerdistance = distance + 50;
        shotLosedistance = distance + 150;

        //What will the enemy ai do on certain distances and conditions:
        //If the player comes close to the enemy, they will trigger them.
        if (!shotsfired)
        {
            if (distance <= defTriggerdistance) //The enemy will be triggered at this distance.
            {
                agent.destination = player.transform.position;    //The enemy goes to the set location.
                triggered = true;
            }
            else if (distance <= defLosedistance) //The enemy will stop pursuing the player at this distance and return back to their default position.
            {
                agent.destination = defaultPos.position;
                triggered = false;
            }
        }
        //If the enemy gets hit by a bullet from a distance, they will be triggered.
        if (shotsfired)
        {
            if (distance <= shotTriggerdistance) //The enemy will be triggered at this distance.
            {
                agent.destination = player.transform.position;    //The enemy goes to the set location.
                triggered = true;
            }
            else if (distance <= shotLosedistance) //The enemy will stop pursuing the player at this distance and return back to their default position.
            {
                agent.destination = defaultPos.position;
                triggered = false;
            }
        }

        //If the player is dead, stop attacking.
        if (playerDead)
        {
            triggered = false;
        }

        //If the enemy is dead, turn everything off and die.
        if (dead)
        {
            jumping = false;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            dirtParticle.SetActive(false);
            attackCollider.SetActive(false);
            anim.SetBool("Dead", true);
            Invoke("WaitBeforeDying", 2.5f);
        }

        //If the enemy reaches its destination, it's velocity will be 0.
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.velocity = Vector3.zero;
            if (triggered == false && !dead)  //When the enemy is not triggered, they will play an idle animation.
            {
                anim.SetInteger("State", 0);
            }
            if (triggered == true && !dead)  //When the enemy is triggered, they will punch the player in close range.
            {
                anim.SetInteger("State", 2);
                attackCollider.SetActive(true);
            }
        }
        else if (agent.velocity != Vector3.zero) //If the enemy is moving.
        {
            anim.SetInteger("State", 1);
        }

        //If the enemy is damaged, play the damaged animation.
        if (damaged && !damagedelay)  
        {
            anim.SetTrigger("Damaged1");
            damagedelay = true;
            StartCoroutine("DamageDelayEnemy");
        }

        //Check if the enemy is ready to jump (passes the jumping bool value to the jumping script).
        if (agent.remainingDistance <= 15 && agent.remainingDistance >=4 && !dead)
        {
            anim.SetInteger("State", 3);
            jumping = true;
        }
        else
        {
            jumping = false;
        }
    }
    //To stop the animation from constantly playing.
    IEnumerator DamageDelayEnemy()
    {
        yield return new WaitForSeconds(0.1f);
        damagedelay = false;
    }

    //Waits before the enemy disappears after dying.
    void WaitBeforeDying()
    {
        gameObject.SetActive(false);
        if (!pickupInstantiated)
        {
            var newHealth = Instantiate(healthPickup, pickupLocation, transform.rotation);
            pickupInstantiated = true;
        }
    }
}
