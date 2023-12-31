using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurret_Boss : MonoBehaviour
{
    public AudioSource flameSound;

    public GameObject flameParticle;
    public GameObject flame;
    public GameObject explosionParticle;
    public GameObject turretBoss;
    public GameObject weaponPickup;
    bool isDead;

    // Update is called once per frame
    void Update()
    {
        isDead = turretBoss.GetComponent<EnemyStats>().isDead;

        if (isDead)
        {
            var explosion = Instantiate(explosionParticle, transform.position, transform.rotation);
            var weapon = Instantiate(weaponPickup, transform.position, transform.rotation);
            gameObject.SetActive(false);
            flame.SetActive(false);
            flameParticle.GetComponent<ParticleSystem>().Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            flameSound.Play();
            flame.SetActive(true);
            flameParticle.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            flameSound.Stop();
            flame.SetActive(false);
            flameParticle.GetComponent<ParticleSystem>().Stop();
        }
    }
}
