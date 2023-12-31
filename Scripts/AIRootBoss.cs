using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is the Root Boss AI.
public class AIRootBoss : MonoBehaviour
{
    public AudioSource plBurrowSound1;
    public AudioSource plBurrowSound2;
    public AudioSource plBurrowSound3;

    public AudioSource burrowSound;
    public AudioSource houseDestroy1;
    public AudioSource houseDestroy2;
    public AudioSource houseDestroy3;
    public AudioSource houseDestroy4;

    public AudioSource roarSound;

    bool plSound1 = false;
    bool plSound2 = false;

    bool burrowed;
    bool spawn;
    bool spawned = false;
    GameObject player;
    public GameObject boss;
    public GameObject bossHitBox;
    public GameObject spawnCollider;
    public GameObject house;
    public GameObject houseParticle;
    public GameObject groundParticle;
    Animator anim;
    bool isDead = false;
    public bool isActive;
    bool playerDead;

    //For pulling
    float timerToPull = 30f;
    float timerToPull2 = 2f;
    float timerToPull3 = 3f;
    bool pull = false;
    bool pulling = false;
    bool pullingEnd = false;
    public GameObject farmCollider;
    bool inRange;
    public GameObject pullLocation;
    public GameObject pullLocation2;
    public GameObject pullLocation3;
    public GameObject pullLocation4;
    float randomPull;

    public GameObject pullParticle1;
    public GameObject pullParticle2;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        spawn = spawnCollider.GetComponent<ControllerCollider>().collided;
        playerDead = player.GetComponent<PlayerStats>().isDead;
        inRange = farmCollider.GetComponent<FarmCollision>().inRange;
        isDead = bossHitBox.GetComponent<EnemyStats>().isDead;

        if(spawn && !spawned)
        {
            burrowSound.Play();
            houseDestroy1.Play();
            houseDestroy2.Play();
            houseDestroy3.Play();
            houseDestroy4.Play();

            house.SetActive(false);
            houseParticle.SetActive(true);
            groundParticle.GetComponent<ParticleSystem>().Play();
            anim.SetTrigger("Spawn");
            anim.SetBool("Idle", true);
            spawned = true;
            isActive = true;
        }

        if (isDead && !burrowed)
        {
            isActive = false;
            anim.SetTrigger("Burrow");
            boss.GetComponent<LookAtPlayer>().enabled = false;
            burrowed = true;
        }

        if (burrowed)
        {
            anim.SetBool("Dead", true);
        }

        //Time until the boss pulls the player towards themselves.
        if(isActive && !pull && !playerDead && inRange)
        {
            timerToPull -= Time.deltaTime;
            pullingEnd = false;
            if (timerToPull <= 0.0f)
            {
                timerToPull = 30f;
                pull = true;
            }
        }
        //The pulling process
        if(pull && !pulling)
        {
            if(!plSound1)
            {
                plBurrowSound1.Play();
                plSound1 = true;
            }

            player.GetComponent<Move>().enabled = false;
            pullParticle1.GetComponent<ParticleSystem>().Play();

            timerToPull2 -= Time.deltaTime;
            if (timerToPull2 <= 0.0f)
            {
                timerToPull2 = 2f;
                pulling = true;
            }
            
        }
        if(pulling && !pullingEnd)
        {
            if (!plSound2)
            {
                plBurrowSound2.Play();
                plSound2 = true;
            }

            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 4f, player.transform.position.z);
            pullParticle2.GetComponent<ParticleSystem>().Play();

            timerToPull3 -= Time.deltaTime;
            if (timerToPull3 <= 0.0f)
            {
                plBurrowSound3.Play();
                //Random location to pull to
                randomPull = Random.Range(1, 5);

                if (randomPull == 1)
                {
                    player.transform.position = pullLocation.transform.position;
                }
                if (randomPull == 2)
                {
                    player.transform.position = pullLocation2.transform.position;
                }
                if (randomPull == 3)
                {
                    player.transform.position = pullLocation3.transform.position;
                }
                if (randomPull == 4)
                {
                    player.transform.position = pullLocation4.transform.position;
                }

                player.GetComponent<Move>().enabled = true;
                pullParticle1.GetComponent<ParticleSystem>().Stop();
                pullParticle2.GetComponent<ParticleSystem>().Stop();

                timerToPull3 = 3f;
                pull = false;
                pulling = false;
                pullingEnd = true;
                plSound1 = false;
                plSound2 = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            roarSound.Play();
            anim.SetTrigger("Attack");
            anim.SetBool("Attacking", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("Attacking", false);
        }
    }
}
