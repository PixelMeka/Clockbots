using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the behaviour of the turnip enemies
public class AITurnip : MonoBehaviour
{
    bool hit;
    public bool ready;
    bool burrowed;
    bool damaged;
    bool damagedelay = false;
    Animator anim;
    public GameObject readyTrigger;
    public GameObject turnipCollider;
    public GameObject turnipDeadBody;
    bool isDead;
    bool shot;
    bool alreadyShot = false;
    public bool shootNow = false;
    bool shotAnim = false;

    float timerToShoot = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("Appear");
    }

    // Update is called once per frame
    void Update()
    {
        isDead = turnipCollider.GetComponent<EnemyStats>().isDead;
        damaged = turnipCollider.GetComponent<EnemyStats>().damaged;
        ready = readyTrigger.GetComponent<AITurnipReadyTrigger>().ready;
        shot = GetComponent<AIShoot>().shot;

        if (ready)
        {
            anim.SetBool("Ready", true);
        }
        
        if(!ready)
        {
            anim.SetBool("Ready", false);
        }

        if(isDead && !burrowed)
        {
            anim.SetTrigger("Burrow");
            readyTrigger.SetActive(false);
            gameObject.GetComponent<LookAtPlayer>().enabled = false;
            gameObject.GetComponent<AIShoot>().enabled = false;
            burrowed = true;
        }

        if(burrowed)
        {
            anim.SetBool("Dead", true);
            turnipCollider.SetActive(false);
            turnipDeadBody.SetActive(true);
        }

        //If the enemy is damaged, play the damaged animation.
        if (damaged && !damagedelay && !isDead)
        {
            anim.SetTrigger("Damaged");
            damagedelay = true;
            StartCoroutine("DamageDelayEnemy");
        }

        //For shooting
        if (shot && !alreadyShot && !isDead)
        {
            if(!shotAnim)
            {
                anim.SetTrigger("Shoot");
                shotAnim = true;
            }
            
            timerToShoot -= Time.deltaTime;
            if (timerToShoot <= 0.0f)
            {
                timerToShoot = 0.3f;
                shootNow = true;
                alreadyShot = true;
                StartCoroutine("WaitToShoot");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !hit && ready)
        {
            anim.SetTrigger("Attack");
            hit = true;
            StartCoroutine("WaitToHit");
        }
    }

    //Pauses between hits.
    IEnumerator WaitToHit()
    {
        yield return new WaitForSeconds(0.8f);
        hit = false;
    }

    //Pauses between shots.
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(4f);
        alreadyShot = false;
        shootNow = false;
        shotAnim = false;
    }

    //To stop the animation from constantly playing.
    IEnumerator DamageDelayEnemy()
    {
        yield return new WaitForSeconds(0.1f);
        damagedelay = false;
    }
}
