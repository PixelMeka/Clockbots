using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is controlled by an animator controller.
public class AIRootBossAttack : MonoBehaviour
{
    public AudioSource burrowSound;
    public AudioSource growlSound;
    public AudioSource spitSound;
    public AudioSource deathSound;
    bool deathS = false;

    public GameObject attackCollider;
    public GameObject attackParticle1;
    public GameObject attackParticle2;
    float random;
    public GameObject groundParticle;
    void Attack()
    {
        growlSound.Play();
        spitSound.Play();
        random = Random.Range(1, 3);
        if(random == 1)
        {
            attackParticle1.GetComponent<ParticleSystem>().Play();
        }

        if(random == 2)
        {
            attackParticle2.GetComponent<ParticleSystem>().Play();
        }

        attackCollider.SetActive(true);
    }

    void NoAttack()
    {
        spitSound.Stop();
        attackParticle1.GetComponent<ParticleSystem>().Stop();
        attackParticle2.GetComponent<ParticleSystem>().Stop();

        attackCollider.SetActive(false);
    }

    void Burrow()
    {
        burrowSound.Play();
        groundParticle.GetComponent<ParticleSystem>().Play();
    }

    void Appear()
    {
        growlSound.Play();
    }

    void Death()
    {
        if(!deathS)
        {
            deathSound.Play();
            deathS = true;
        }
    }
}
