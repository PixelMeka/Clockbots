using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script enables and disables the attack collider of the root. (Also plays some particle effectss) It is controlled by an animation event.
public class AIRootAttack : MonoBehaviour
{
    public AudioSource appearSound;
    public AudioSource burrowSound;
    public AudioSource whipSound;
    public AudioSource spawnSound;

    public GameObject attackCollider;
    public GameObject burrowParticle;
    public GameObject spawnParticle;

    void Attack()
    {
        whipSound.Play();
        attackCollider.SetActive(true);
    }

    void NoAttack()
    {
        attackCollider.SetActive(false);
    }

    void Spawn()
    {
        spawnSound.Play();
        spawnParticle.GetComponent<ParticleSystem>().Play();
    }

    void SpawnEnd()
    {
        spawnSound.Stop();
        spawnParticle.GetComponent<ParticleSystem>().Stop();
    }

    void Burrow()
    {
        burrowSound.Play();
        burrowParticle.GetComponent<ParticleSystem>().Play();
    }
}
