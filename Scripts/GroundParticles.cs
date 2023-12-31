using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundParticles : MonoBehaviour
{
    public bool isGrounded = true;
    public GameObject dirt;

    // Start is called before the first frame update
    void Awake()
    {
        dirt.SetActive(true);
    }

    void Update()
    {
        if(isGrounded)
        {
            dirt.SetActive(true);
        }
        if(!isGrounded)
        {
            dirt.SetActive(false);
        }
    }

    //Check if the object is on the ground
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
