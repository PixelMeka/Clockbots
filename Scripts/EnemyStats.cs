using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the health of enemies.
public class EnemyStats : MonoBehaviour
{
    public AudioSource hitSound;

    public float curHealth;
    public float maxHealth;
    public bool isDead;
    bool playerDead = false;
    GameObject player;
    public bool damaged;
    public bool shotsfired = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerDead = player.GetComponent<PlayerStats>().isDead;

        if (curHealth > maxHealth || playerDead)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            isDead = true;
        }
        if (curHealth > 0)
        {
            isDead = false;
        }

        //To reset trigger and lose distances.
        if (playerDead)
        {
            shotsfired = false;
        }
    }

    //Enemies will get hit and lose health:
    private void OnTriggerEnter(Collider other)
    {
        if(!isDead)
        {
            if (other.tag == "Player Punch" && !damaged)
            {
                hitSound.Play();

                curHealth -= 20;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
            }

            if (other.tag == "Super Punch" && !damaged)
            {
                hitSound.Play();
                curHealth -= 40;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
            }

            if (other.tag == "Player Bullet" && !damaged)
            {
                curHealth -= 10;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
                shotsfired = true;
            }

            if(other.tag == "Player Fast Bullet" && !damaged)
            {
                curHealth -= 4;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
                shotsfired = true;
            }

            if (other.tag == "Super Bullet" && !damaged)
            {
                curHealth -= 20;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
                shotsfired = true;
            }

            if(other.tag == "Super Fast Bullet" && !damaged)
            {
                curHealth -= 8;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
                shotsfired = true;
            }

            if (other.tag == "Player Pellet" && !damaged)
            {
                curHealth -= 10;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
                shotsfired = true;
            }

            if (other.tag == "Player Explosion" && !damaged)
            {
                curHealth -= 20;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
                shotsfired = true;
            }
        }
    }

    
    //To stop the enemy from taking too much damage from a single hit, and to create a delay.
    IEnumerator DamageDelayEnemy()
    {
        yield return new WaitForSeconds(0.02f);
        damaged = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!isDead)
        {
            if (other.tag == "Fire" && !damaged)
            {
                curHealth -= 2;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
                shotsfired = true;
            }

            if (other.tag == "Superfire" && !damaged)
            {
                curHealth -= 3;
                damaged = true;
                StartCoroutine("DamageDelayEnemy");
                shotsfired = true;
            }
        }
    }
}
