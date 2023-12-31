using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOff : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    //To turn the fog off
    private void OnTriggerEnter(Collider other)
    {
        RenderSettings.fog = false;
    }
}
