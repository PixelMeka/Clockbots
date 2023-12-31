using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform spawnPoint;

    //For respawning
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
}
