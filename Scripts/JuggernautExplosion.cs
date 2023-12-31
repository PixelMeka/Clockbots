using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is controlled by an animator event.
public class JuggernautExplosion : MonoBehaviour
{
    public GameObject explosionAura;
    public GameObject explosionParticle;

    void Start()
    {
        explosionAura.SetActive(false);
        explosionParticle.SetActive(false);
    }
    //When the enemy explodes
    public void ExplodeJ()
    {
        explosionAura.SetActive(true);
        explosionParticle.SetActive(true);
    }
    //When the explosion is over
    public void ExplodeJOver()
    {
        explosionAura.SetActive(false);
    }
}
