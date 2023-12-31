using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmCollision : MonoBehaviour
{
    public bool inRange;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
}
