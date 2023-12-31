using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is the enemy roots' AI. They will spawn other enemies.
public class AIRoot : MonoBehaviour
{
    GameObject deadCollider;
    bool playerDead;
    bool spawn;
    bool spawned;
    bool firstSpawn = false;
    bool secondSpawn = true;
    public GameObject parentRoot; //Will only spawn if the parent root is dead.
    public GameObject groundParticle;
    public GameObject root;
    public GameObject rootLooker;
    Animator anim;
    bool isActive = false;
    bool burrowed = false;
    bool isDead;
    bool spawnEnemies = false;
    bool spawnEnemies2 = false;
    bool standUp= false;

    //For spawning
    float timerToSpawn = 5f;
    float timerToStand = 7f;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public Transform enemy3Pos;

    // Start is called before the first frame update
    void Start()
    {
        anim = root.GetComponent<Animator>();
        root.GetComponent<CapsuleCollider>().enabled = false;
        rootLooker.GetComponent<LookAtPlayer>().enabled = false;
        deadCollider = GameObject.FindWithTag("Death Level");
    }

    // Update is called once per frame
    void Update()
    {
        isDead = root.GetComponent<EnemyStats>().isDead;
        spawn = parentRoot.GetComponent<EnemyStats>().isDead;
        playerDead = deadCollider.GetComponent<FarmCollision>().inRange;

        if (spawn && !spawned)
        {
            rootLooker.GetComponent<LookAtPlayer>().enabled = true;
            groundParticle.GetComponent<ParticleSystem>().Play();
            anim.SetTrigger("Spawn");
            anim.SetBool("Idle", true);
            spawned = true;
            isActive = true;
            spawnEnemies = true;
            root.GetComponent<CapsuleCollider>().enabled = true;
        }

        if (isDead && !burrowed)
        {
            isActive = false;
            anim.SetTrigger("Burrow");
            rootLooker.GetComponent<LookAtPlayer>().enabled = false;
            burrowed = true;
        }

        if (burrowed)
        {
            anim.SetBool("Dead", true);
            root.GetComponent<CapsuleCollider>().enabled = false;
            root.GetComponent<EnemyStats>().enabled = false;
        }

        //Spawns enemies after a set amount of time
        if(spawnEnemies && !isDead)
        {
            timerToSpawn -= Time.deltaTime;
            if (timerToSpawn <= 0.0f)
            {
                anim.SetTrigger("Bend");
                anim.SetBool("Spawning", true);
                spawnEnemies = false;
                standUp = true;

                timerToSpawn = 20f;
            }
        }
        if(standUp)
        {
            timerToStand -= Time.deltaTime;
            if (timerToStand <= 0.0f)
            {
                if(!firstSpawn)
                {
                    enemy1.SetActive(true);
                    enemy2.SetActive(true);
                    firstSpawn = true;
                }

                //For continuous spawning
                if (!secondSpawn)
                {
                    var enemy = Instantiate(enemy3, enemy3Pos.position, transform.rotation);
                    secondSpawn = true;
                }

                timerToStand = 7f;
                anim.SetBool("Spawning", false);
                standUp = false;
                spawnEnemies2 = true;
            }
        }

        //Second continuous spawning
        if(spawnEnemies2 && !isDead && !playerDead)
        {
            timerToSpawn -= Time.deltaTime;
            if (timerToSpawn <= 0.0f)
            {
                anim.SetTrigger("Bend");
                anim.SetBool("Spawning", true);
                spawnEnemies2 = false;
                standUp = true;
                secondSpawn = false;

                timerToSpawn = 20f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("Attack");
        }
    }
}
