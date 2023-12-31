using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCollider : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "EnemyM" || other.tag == "EnemyP" || other.tag == "EnemyT")
        {
            player.GetComponent<PlayerStats>().fireHit = true;
        }
    }
}
