using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping : MonoBehaviour
{
    public AudioSource sleepSound1;
    public AudioSource sleepSound2;

    public void Sleep1()
    {
        sleepSound1.Play();
    }

    public void Sleep2()
    {
        sleepSound2.Play();
    }
}
