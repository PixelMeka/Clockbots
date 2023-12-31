using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is controlled by an animator event.
public class PumpkinExplosion : MonoBehaviour
{
    public GameObject explosionAura;
    public GameObject explosionParticle;
    public GameObject fireParticle;

    public AudioSource fireSoundP;
    public AudioSource fireSound;
    public AudioSource fireSound2;
    public AudioSource explodeSoundSquish;

    void Start()
    {
        explosionAura.SetActive(false);
        explosionParticle.SetActive(false);
    }
    //When the enemy explodes
    public void ExplodeP()
    {
        explodeSoundSquish.Play();

        explosionAura.SetActive(true);
        explosionParticle.SetActive(true);
    }
    //When the explosion is over
    public void ExplodeOver()
    {
        explosionAura.SetActive(false);
    }

    public void FireStart()
    {
        fireSoundP.Play();
        fireSound.Play();
        fireSound2.Play();

        fireParticle.GetComponent<ParticleSystem>().Play();
    }

    public void FireEnd()
    {
        fireSoundP.Stop();
        fireSound.Stop();
        fireSound2.Stop();
        fireParticle.GetComponent<ParticleSystem>().Stop();
    }
}
