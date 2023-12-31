using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script enables and disables the attack collider of the turnip. (Also plays some particle effectss) It is controlled by an animation event.
public class AITurnipAttack : MonoBehaviour
{
    public GameObject attackCollider;
    public GameObject attackParticle;
    public GameObject burrowParticle;

    public AudioSource lickSound;
    public AudioSource burrowSound;
    public AudioSource appearSound;

    void Spit()
    {
        attackParticle.GetComponent<ParticleSystem>().Play();
    }

   void Attack()
    {
        attackCollider.SetActive(true);
    }

    void NoAttack()
    {
        attackCollider.SetActive(false);
    }

    void Burrow()
    {
        burrowSound.Play();
        burrowParticle.GetComponent<ParticleSystem>().Play();
    }

    void Appear()
    {
        appearSound.Play();
    }

    void LickSound()
    {
        lickSound.Play();
    }
}
