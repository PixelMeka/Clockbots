using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is called by the animation event, it simulates the slow downs and speed ups during the clockbot walk cycle.
public class JuggernautWalk : MonoBehaviour
{
    public AudioSource walksound1;
    public AudioSource walksound2;
    public AudioSource slamsound;

    public NavMeshAgent agent;

    public void SpeedUp()
    {
        agent.speed = 4.5f;
    }
    public void SlowDown()
    {
        agent.speed = 0.01f;
    }

    public void WalkC1()
    {
        walksound1.Play();
    }

    public void WalkC2()
    {
        walksound2.Play();
    }

    public void Slam()
    {
        slamsound.Play();
    }
}
