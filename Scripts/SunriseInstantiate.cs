using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunriseInstantiate : MonoBehaviour
{
    [SerializeField]
    private Transform sunrise;

    [SerializeField]
    private Transform sunSpawnPoint;

    //When the player collides with that object
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sunrise.transform.position = sunSpawnPoint.transform.position;
        }
    }
}
