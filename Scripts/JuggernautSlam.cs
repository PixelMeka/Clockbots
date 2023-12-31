using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script activates and deactivates Juggernaut's attack collider. It is controlled by an animation event.
public class JuggernautSlam : MonoBehaviour
{
    public GameObject attackCollider;

    void Start()
    {
        attackCollider.SetActive(false);
    }
    void Slam()
    {
        attackCollider.SetActive(true);
    }

    void NoSlam()
    {
        attackCollider.SetActive(false);
    }

}
