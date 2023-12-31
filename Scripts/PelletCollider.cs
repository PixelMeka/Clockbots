using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletCollider : MonoBehaviour
{
    public bool addSuperPower; //Bullets marked with this will increase the player's super power value
    GameObject player;

    //Particle effects to simulate collisions with different objects
    public GameObject damager;
    public GameObject defaultCollision;
    public GameObject superCollision;
    public GameObject metalCollision;
    public GameObject groundCollision;
    public GameObject pumpkinCollision;
    public GameObject stoneCollision;
    public GameObject woodCollision;
    public GameObject explosion;
    float timerexplosion = 3f;
    float timerexplosionSticky = 4f;
    float timerexplosion2 = 0.05f;
    bool exploded = false;
    public bool sticky;
    Rigidbody rigy;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Physics.IgnoreLayerCollision(10, 11); //Layer 10 = Bullet, Layer 11 = Invisible Wall. This will let the bullet pass through invisible walls.
        rigy = GetComponent<Rigidbody>();
    }
    //Destroys the bullet if it collides with anything and hit returns true if it collides with an enemy (used for the super power meter).
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyM")
        {
            if (addSuperPower)
            {
                player.GetComponent<PlayerStats>().pelletHit = true;
            }
            gameObject.SetActive(false);
            var explosionPellet = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (collision.gameObject.tag == "EnemyP")
        {
            if (addSuperPower)
            {
                player.GetComponent<PlayerStats>().pelletHit = true;
            }
            gameObject.SetActive(false);
            var explosionPellet = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (collision.gameObject.tag == "EnemyT")
        {
            if (addSuperPower)
            {
                player.GetComponent<PlayerStats>().bulletHit = true;
            }
            gameObject.SetActive(false);
            var explosionPellet = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (collision.gameObject.tag == "Metal")
        {
            if (sticky)
            {
                rigy.isKinematic = true;
            }

            var hitMetal = Instantiate(metalCollision, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (collision.gameObject.tag == "Pumpkin" || collision.gameObject.tag == "Hazard")
        {
            if (sticky)
            {
                rigy.isKinematic = true;
            }

            var hitPumpkin = Instantiate(pumpkinCollision, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            if (sticky)
            {
                rigy.isKinematic = true;
            }

            var hitGround = Instantiate(groundCollision, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (collision.gameObject.tag == "Stone")
        {
            if (sticky)
            {
                rigy.isKinematic = true;
            }

            var hitStone = Instantiate(stoneCollision, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (collision.gameObject.tag == "Wood")
        {
            if (sticky)
            {
                rigy.isKinematic = true;
            }

            var hitWood = Instantiate(woodCollision, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if(collision.gameObject.tag == "Turnip")
        {
            if (sticky)
            {
                rigy.isKinematic = true;
            }

            var hitSuper = Instantiate(superCollision, gameObject.transform.position, gameObject.transform.rotation);
        }
        else
        {
            if (sticky)
            {
                rigy.isKinematic = true;
            }

            var hitDefault = Instantiate(defaultCollision, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
    //Destroys the pellet when the timer runs out.
    void Update()
    {
        if(!sticky)
        {
            timerexplosion -= Time.deltaTime;
            if (timerexplosion <= 0.0f)
            {
                if (!exploded)
                {
                    damager.SetActive(true);
                    exploded = true;
                }
            }

            if (exploded)
            {
                timerexplosion2 -= Time.deltaTime;
                if (timerexplosion2 <= 0.0f)
                {
                    gameObject.SetActive(false);
                    var explosionPellet = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                }
            }
        }
        else if(sticky)
        {
            timerexplosionSticky -= Time.deltaTime;
            if (timerexplosionSticky <= 0.0f)
            {
                if (!exploded)
                {
                    damager.SetActive(true);
                    exploded = true;
                }
            }

            if (exploded)
            {
                timerexplosion2 -= Time.deltaTime;
                if (timerexplosion2 <= 0.0f)
                {
                    gameObject.SetActive(false);
                    var explosionPellet = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                }
            }
        }
    }
}