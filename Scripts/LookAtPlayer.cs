using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    GameObject player;
    public int type; //For shooting (2) vs for other things (1)

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    //Makes the npc face the player
    void Update()
    {
        if(type == 1)
        {
            Vector3 place = new Vector3(player.transform.position.x - this.transform.position.x, 0f, player.transform.position.z - this.transform.position.z); //The place to look at, y is neglected.
            Quaternion rot = Quaternion.LookRotation(place); //Tells the npc to literally look at that place.
            this.transform.rotation = rot; //Rotate the gameobject to that place.
        }

        //For shooting
        if(type == 2)
        {
            Vector3 place = new Vector3(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y, player.transform.position.z - this.transform.position.z);
            Quaternion rot = Quaternion.LookRotation(place); //Tells the npc to literally look at that place.
            this.transform.rotation = rot; //Rotate the gameobject to that place.
        }
    }
}
