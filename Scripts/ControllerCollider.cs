using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollider : MonoBehaviour
{
    public bool collided = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            collided = true;
            gameObject.SetActive(false);
        }
    }
}
