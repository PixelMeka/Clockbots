using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleAnimationEvent : MonoBehaviour
{
    public AudioSource downSound;
    public AudioSource upSound;

    void Down()
    {
        downSound.Play();
    }

    void Up()
    {
        upSound.Play();
    }
}
