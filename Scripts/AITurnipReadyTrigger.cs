using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurnipReadyTrigger : MonoBehaviour
{
    public bool ready = false;
    public GameObject turnip;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            turnip.GetComponent<LookAtPlayer>().enabled = true;
            ready = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            turnip.GetComponent<LookAtPlayer>().enabled = false;
            ready = false;
        }
    }
}
