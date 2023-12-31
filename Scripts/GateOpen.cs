using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    public AudioSource openSound;

    void Open()
    {
        openSound.Play();
    }
}
