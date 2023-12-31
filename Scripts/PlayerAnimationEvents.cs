using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public AudioSource landAudio;

    public AudioSource step1;
    public AudioSource step2;
    public AudioSource step3;
    public AudioSource step4;
    public AudioSource wepSwitch;

    public AudioSource flameRelSound1;
    public AudioSource flameRelSound2;
    public AudioSource flameRelSound3;

    bool stepPlayed = false;

    int rand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Land()
    {
        landAudio.Play();
    }

    void Walk()
    {
        rand = Random.Range(1, 5);

        if(rand == 1)
        {
            step1.Play();
        }
        if (rand == 2)
        {
            step2.Play();
        }
        if (rand == 3)
        {
            step3.Play();
        }
        if (rand == 4)
        {
            step4.Play();
        }
    }

    void Switch()
    {
        wepSwitch.Play();
    }

    void FlameReload1()
    {
        flameRelSound1.Play();
    }

    void FlameReload2()
    {
        flameRelSound2.Play();
    }

    void FlameReload3()
    {
        flameRelSound2.Stop();
        flameRelSound3.Play();
    }
}
