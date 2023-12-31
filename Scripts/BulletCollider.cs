using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    public bool addSuperPower; //Bullets marked with this will increase the player's super power value
    GameObject player;

    //Particle effects to simulate collisions with different objects
    public GameObject defaultCollision;
    public GameObject superCollision;
    public GameObject metalCollision;
    public GameObject groundCollision;
    public GameObject pumpkinCollision;
    public GameObject stoneCollision;
    public GameObject woodCollision;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Physics.IgnoreLayerCollision(10, 11); //Layer 10 = Bullet, Layer 11 = Invisible Wall. This will let the bullet pass through invisible walls.
    }
    //Destroys the bullet if it collides with anything and hit returns true if it collides with an enemy (used for the super power meter).
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyM")
        {
            var hitMetal = Instantiate(metalCollision, gameObject.transform.position, gameObject.transform.rotation);
            if (addSuperPower)
            {
                player.GetComponent<PlayerStats>().bulletHit = true;
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "EnemyP")
        {
            var hitPumpkin = Instantiate(pumpkinCollision, gameObject.transform.position, gameObject.transform.rotation);
            if (addSuperPower)
            {
                player.GetComponent<PlayerStats>().bulletHit = true;
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "EnemyT")
        {
            var hitTurnip = Instantiate(superCollision, gameObject.transform.position, gameObject.transform.rotation);
            if (addSuperPower)
            {
                player.GetComponent<PlayerStats>().bulletHit = true;
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Metal")
        {
            var hitMetal = Instantiate(metalCollision, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Pumpkin" || collision.gameObject.tag == "Hazard")
        {
            var hitPumpkin = Instantiate(pumpkinCollision, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            var hitGround = Instantiate(groundCollision, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Stone")
        {
            var hitStone = Instantiate(stoneCollision, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Wood")
        {
            var hitWood = Instantiate(woodCollision, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Turnip")
        {
            var hitSuper = Instantiate(superCollision, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            var hitDefault = Instantiate(defaultCollision, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
    //Destroys the bullet after ~10 seconds if it doesn't collide with anything.
    void Update()
    {
        Destroy(gameObject, 5);
    }

}
