using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script makes the pumpkin jump
public class PumpkinJump : MonoBehaviour
{
    public AudioSource walksound1;
    public AudioSource walksound2;
    public AudioSource bitesound;
    public AudioSource appearSound;

    float curHeight;
    public float maxHeight;
    public float minHeight;
    public float jumpspeed1;
    public float jumpspeed2;
    bool falling = false;
    bool jumping;
    public GameObject pumpkin;

    // Update is called once per frame
    void Update()
    {
        jumping = pumpkin.GetComponent<AIEnemy_Jumper>().jumping; //Gets the jumping bool value from the AI script

        //The y component of this object's transform will gradually increase and decrease.
        if (jumping)
        {
            curHeight += jumpspeed1 * Time.deltaTime;
            curHeight = Mathf.Clamp(curHeight, minHeight, maxHeight);
            this.transform.localPosition = new Vector3(0, curHeight, 0);
            if(curHeight >= maxHeight)
            {
                falling = true;
            }
        }
        if(falling)
        {
            curHeight -= jumpspeed2 * Time.deltaTime;
            curHeight = Mathf.Clamp(curHeight, minHeight, maxHeight);
            this.transform.localPosition = new Vector3(0, curHeight, 0);
            if(curHeight <= minHeight)
            {
                falling = false;
            }
        }
        else if(!jumping && transform.position.y != 0) //Fixes the pumpkin getting stuck in the air mid jump.
        {
            curHeight -= jumpspeed2 * Time.deltaTime;
            curHeight = Mathf.Clamp(curHeight, minHeight, maxHeight);
            this.transform.localPosition = new Vector3(0, curHeight, 0);
        }
    }

    void WalkP1()
    {
        walksound1.Play();
    }

    void WalkP2()
    {
        walksound2.Play();
    }

    void Bite()
    {
        bitesound.Play();
    }

    void Appear()
    {
        appearSound.Play();
    }
}
