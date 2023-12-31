using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIClockbot : MonoBehaviour
{
    public AudioSource standUpSound;
    public AudioSource deathSound;
    bool isdeathSound = false;

    public int type; //Specifiy if that particular enemy is a shooter or not.
    NavMeshAgent agent;
    GameObject player;
    public Transform defaultPos;
    public GameObject attackCollider;
    public GameObject healthPickup;
    public GameObject smokeLand;
    public GameObject poofParticle;
    Collider col;
    Animator anim;
    float distance;
    public bool triggered = false;
    public bool isGrounded;
    bool landed;
    Rigidbody rigy;
    bool damaged;
    public bool aiming;
    bool shot;
    bool alreadyShot = false;
    bool damagedelay = false;
    bool tired = false;
    float timerCollision = 1.5f;
    float timerJuggernautShoot = 5f;
    float timerJuggernautCD = 8f;
    float timerJuggernautGReady = 1f;
    float timerJuggernautSlam = 1f;
    bool ready = false;
    bool startShooting;

    bool playerDead;
    bool dead = false;

    //For the pickup system
    bool pickupInstantiated = false;
    Vector3 pickupLocation;
    float pickupY;

    //For the random numbers
    int landanim;
    int deathanim;
    int damagedanim;

    //Different trigger and lose distances
    public float defTriggerdistance = 150;
    public float defLosedistance = 300;
    float shotTriggerdistance;
    float shotLosedistance;
    bool shotsfired;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
        isGrounded = false;
        landed = false;
        rigy = GetComponent<Rigidbody>();
        col = gameObject.GetComponent<BoxCollider>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position); //Distance between the enemy and the target.
        playerDead = player.GetComponent<PlayerStats>().isDead;
        dead = GetComponent<EnemyStats>().isDead;
        pickupY = gameObject.transform.position.y + 3.33f;
        pickupLocation = new Vector3(this.transform.position.x, pickupY, this.transform.position.z);
        damaged = GetComponent<EnemyStats>().damaged;
        shotsfired = GetComponent<EnemyStats>().shotsfired;

        timerCollision -= Time.deltaTime;
        if (timerCollision <= 0.0f)
        {
            col.enabled = true;
        }

        //If the enemy gets shot from a distance, they will still start pursuing their target.
        shotTriggerdistance = distance + 50;
        shotLosedistance = distance + 150;

        //Different AI will work depending on which enemy type is selected.
        if(type == 1)
        {
            attackCollider.SetActive(false);
            Melee();
        }

        if(type == 2)
        {
            attackCollider.SetActive(false);
            Shooter();
        }

        if(type == 3)
        {
            Juggernaut();
        }
    }

    //For melees
    void Melee()
    {
        //This will happen when the enemy spawns in the air.
        if (!isGrounded && !landed)
        {
            anim.SetInteger("State", 4);
        }

        //This will happen when the enemy is on the ground.
        if (isGrounded && landed)
        {
            //If the enemy is damaged, play the damaged animation.
            if (damaged && !damagedelay)
            {
                damagedanim = Random.Range(1, 4);
                if (damagedanim == 1)
                {
                    anim.SetTrigger("Damaged1");
                    damagedelay = true;
                    StartCoroutine("DamageDelayEnemy");
                }
                else if (damagedanim == 2)
                {
                    damagedelay = true;
                    anim.SetTrigger("Damaged2");
                    StartCoroutine("DamageDelayEnemy");
                }
                else if (damagedanim == 3)
                {
                    damagedelay = true;
                    anim.SetTrigger("Damaged3");
                    StartCoroutine("DamageDelayEnemy");
                }
            }

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
                    agent.destination = defaultPos.transform.position;
                    triggered = false;
                }
            }
            //If the enemy gets hit by a bullet from a distance, they will be triggered.
            if(shotsfired)
            {
                if (distance <= shotTriggerdistance) //The enemy will be triggered at this distance.
                {
                    agent.destination = player.transform.position;    //The enemy goes to the set location.
                    triggered = true;
                }
                else if (distance <= shotLosedistance) //The enemy will stop pursuing the player at this distance and return back to their default position.
                {
                    agent.destination = defaultPos.transform.position;
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
                if(!isdeathSound)
                {
                    deathSound.Play();
                    isdeathSound = true;
                }

                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                attackCollider.SetActive(false);

                deathanim = Random.Range(1, 3);
                if (deathanim == 1)
                {
                    anim.SetInteger("Dead", 1);
                }

                if (deathanim == 2)
                {
                    anim.SetInteger("Dead", 2);
                }

                Invoke("WaitBeforeDying", 4f);
            }

            //If the enemy reaches its destination, it's velocity will be 0.
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.velocity = Vector3.zero;
                if (triggered == false && !dead)
                {
                    anim.SetInteger("State", 0);
                    shotsfired = false;
                }
                if (triggered == true && !dead)
                {
                    anim.SetInteger("State", 2);
                    attackCollider.SetActive(true);
                }
            }
            else if (agent.velocity != Vector3.zero) //If the enemy is moving.
            {
                anim.SetInteger("State", 1);
            }
        }
    }

    //For shooters
    void Shooter()
    {
        shot = GetComponent<AIShoot>().shot;

        //This will happen when the enemy spawns in the air.
        if (!isGrounded && !landed)
        {
            anim.SetInteger("State", 4);
            aiming = false;
        }

        //This will happen when the enemy is on the ground.
        if (isGrounded && landed)
        {
            //If the enemy is damaged, play the damaged animation.
            if (damaged && !damagedelay)
            {
                damagedanim = Random.Range(1, 4);
                if (damagedanim == 1)
                {
                    anim.SetTrigger("Damaged1");
                    damagedelay = true;
                    StartCoroutine("DamageDelayEnemy");
                }
                else if (damagedanim == 2)
                {
                    damagedelay = true;
                    anim.SetTrigger("Damaged2");
                    StartCoroutine("DamageDelayEnemy");
                }
                else if (damagedanim == 3)
                {
                    damagedelay = true;
                    anim.SetTrigger("Damaged3");
                    StartCoroutine("DamageDelayEnemy");
                }
            }

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
                    agent.destination = defaultPos.transform.position;
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
                    agent.destination = defaultPos.transform.position;
                    triggered = false;
                }
            }

            //If the player is dead, stop attacking.
            if (playerDead)
            {
                triggered = false;
                aiming = false;
            }

            //If the enemy is dead, turn everything off and die.
            if (dead)
            {
                if (!isdeathSound)
                {
                    deathSound.Play();
                    isdeathSound = true;
                }

                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                attackCollider.SetActive(false);
                aiming = false;

                deathanim = Random.Range(1, 3);
                if (deathanim == 1)
                {
                    anim.SetInteger("Dead", 1);
                }

                if (deathanim == 2)
                {
                    anim.SetInteger("Dead", 2);
                }

                Invoke("WaitBeforeDying", 4.1f);
            }

            //For AI shooting
            if(distance <= 200 && agent.remainingDistance >= agent.stoppingDistance && !dead && !playerDead && triggered)
            {
                anim.SetBool("Aim", true);
                aiming = true;
            }
            else
            {
                anim.SetBool("Aim", false);
                aiming = false;
            }

            if(shot && !alreadyShot)
            {
                anim.SetTrigger("Shoot");
                alreadyShot = true;
                StartCoroutine("WaitToShoot");
            }

            //If the enemy reaches its destination, it's velocity will be 0.
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.velocity = Vector3.zero;
                if (triggered == false && !dead)
                {
                    anim.SetInteger("State", 0);
                }
                if (triggered == true && !dead)
                {
                    anim.SetInteger("State", 2);
                    attackCollider.SetActive(true);
                }
            }
            
            if (agent.velocity != Vector3.zero) //If the enemy is moving.
            {
                anim.SetInteger("State", 1);
            }
        }
    }

    //For Juggernauts
    void Juggernaut()
    {
        shot = GetComponent<AIShootJ>().shot;

        //This will happen when the enemy spawns in the air.
        if (!isGrounded && !landed)
        {
            anim.SetInteger("State", 4);
            aiming = false;
        }

        //This will happen when the enemy is on the ground.
        if (isGrounded && landed)
        {
            //If the enemy is damaged, play the damaged animation.
            if (damaged && !damagedelay)
            {
                damagedanim = Random.Range(1, 4);
                if (damagedanim == 1)
                {
                    anim.SetTrigger("Damaged1");
                    damagedelay = true;
                    StartCoroutine("DamageDelayEnemy");
                }
                else if (damagedanim == 2)
                {
                    damagedelay = true;
                    anim.SetTrigger("Damaged2");
                    StartCoroutine("DamageDelayEnemy");
                }
                else if (damagedanim == 3)
                {
                    damagedelay = true;
                    anim.SetTrigger("Damaged3");
                    StartCoroutine("DamageDelayEnemy");
                }
            }

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
                    agent.destination = defaultPos.transform.position;
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
                    agent.destination = defaultPos.transform.position;
                    triggered = false;
                }
            }

            //If the player is dead, stop attacking.
            if (playerDead)
            {
                triggered = false;
                aiming = false;
            }

            //If the enemy is dead, turn everything off and die.
            if (dead)
            {
                if (!isdeathSound)
                {
                    deathSound.Play();
                    isdeathSound = true;
                }

                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                attackCollider.SetActive(false);
                aiming = false;
                tired = true;

                anim.SetInteger("Dead", 1);

                Invoke("WaitBeforeDying", 4.2f);
            }

            //For AI Juggernaut shooting. This enemy will constantly move towards the player but will have to stop in order to be able to shoot for a bit.
            if (distance <= 100 && agent.remainingDistance >= agent.stoppingDistance && !dead && !playerDead && triggered && !tired)
            {
                agent.velocity = Vector3.zero;
                if(!ready)
                {
                    anim.SetTrigger("Aim Start");
                    ready = true;
                }
                anim.SetBool("Aim", true);
                startShooting = true;

                timerJuggernautShoot -= Time.deltaTime; //Shoots until the time runs out
                if (timerJuggernautShoot <= 0.0f)
                {
                    timerJuggernautShoot = 5f;
                    tired = true;
                }
            }
            else if (tired) //Cooldown timer before Juggernaut will be able to shoot again
            {
                anim.SetBool("Aim", false);
                aiming = false;
                timerJuggernautCD -= Time.deltaTime;
                if (timerJuggernautCD <= 0.0f)
                {
                    timerJuggernautCD = 5f;
                    tired = false;
                    ready = false;
                }
            }
            else
            {
                anim.SetBool("Aim", false);
                aiming = false;
            }

            //A delay before the Juggernaut will start shooting
            if(startShooting)
            {
                timerJuggernautGReady -= Time.deltaTime;
                if (timerJuggernautGReady <= 0.0f)
                {
                    timerJuggernautGReady = 1f;
                    aiming = true;
                    startShooting = false;
                }
            }

            if (shot && !alreadyShot)
            {
                anim.SetTrigger("Shoot");
                alreadyShot = true;
                StartCoroutine("WaitToShootJ");
            }

            //If the enemy reaches its destination, it's velocity will be 0.
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.velocity = Vector3.zero;
                if (triggered == false && !dead)
                {
                    anim.SetInteger("State", 0);
                }
                if (triggered == true && !dead && !aiming) //For slamming. If the player comes close, shooting will end prematurely.
                {
                    timerJuggernautShoot = 5f;
                    tired = true;

                    timerJuggernautSlam -= Time.deltaTime; //Pauses between each slam
                    if (timerJuggernautSlam <= 0.0f)
                    {
                        timerJuggernautSlam = 1f;
                        anim.SetTrigger("Slam");
                    }
                }
            }

            if (agent.velocity != Vector3.zero) //If the enemy is moving.
            {
                anim.SetInteger("State", 1);
            }
        }
    }

    //Waits before the enemy disappears after dying.
    void WaitBeforeDying()
    {
        gameObject.SetActive(false);
        
        if (!pickupInstantiated)
        {
            var deadParticle = Instantiate(poofParticle, transform.position, transform.rotation);
            var newHealth = Instantiate(healthPickup, pickupLocation, transform.rotation);
            pickupInstantiated = true;
        }
    }

    //Check if the enemy touches the ground and turns on isKinematic of Rigidbody and NavMeshAgent (used for drop spawns).
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            standUpSound.Play();

            isGrounded = true;
            landanim = Random.Range(1, 3);
            smokeLand.GetComponentInChildren<ParticleSystem>().Play();
            if (landanim == 1)
            {
                anim.SetInteger("Land", 1);
            }

            if(landanim == 2)
            {
                anim.SetInteger("Land", 2);
            }
            Invoke("WaitToLand", 1.3f);
        }
    }

    //Waits for the landing animation to finish before turning on the NavMeshAgent and isKinematic of Rigidbody.
    void WaitToLand()
    {
        agent.GetComponent<NavMeshAgent>().enabled = true;
        rigy.isKinematic = true;
        landed = true;
    }

    //To stop the animation from constantly playing.
    IEnumerator DamageDelayEnemy()
    {
        yield return new WaitForSeconds(0.1f);
        damagedelay = false;
    }

    //Pauses between shots.
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(1.5f);
        alreadyShot = false;
    }
    //Pauses for the Juggernauts
    IEnumerator WaitToShootJ()
    {
        yield return new WaitForSeconds(0.1f);
        alreadyShot = false;
    }
}
