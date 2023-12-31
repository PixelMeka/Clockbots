using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is the AI of the spawner enemies. It makes them move and behave the way they are supposed to.
public class AISpawner : MonoBehaviour
{
    public AudioSource doorSound;
    bool isdoorSound = false;

    public GameObject poofParticle;

    Animator anim;
    float timerToLeave = 3f;
    bool go = true;
    bool leave = false;
    bool close = false;
    bool open = true;
    public float speed = 30f; //The speed of this enemy
    float distance; //The distance to move per frame

    //Where to move:
    public Transform point1ToLookAt; //The first point to look at. Solves some problems.
    public Transform point1;
    public Transform point2;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Slowly move the enemy to the first point. Also turn towards that point.
        if (go)
        {
            distance = speed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, point1.position, distance);

            Vector3 place = new Vector3(point1ToLookAt.transform.position.x - this.transform.position.x, 0, point1ToLookAt.transform.position.z - this.transform.position.z);
            Quaternion rot = Quaternion.LookRotation(place); //Tells the npc to literally look at that place.
            this.transform.rotation = rot; //Rotate the gameobject to that place.
        }
        //If this enemy reaches the first point
        if (this.transform.position == point1.transform.position && !leave) 
        {
            go = false;
            distance = 0;

            if(open && !close)
            {
                anim.SetTrigger("Open");

                if(!isdoorSound)
                {
                    doorSound.Play();
                    isdoorSound = true;
                }
                
            }
            anim.SetBool("Wait", true);

            timerToLeave -= Time.deltaTime;
            if (timerToLeave <= 0.0f)
            {
                anim.SetBool("Wait", false);
                close = true;
                open = false;
                isdoorSound = false;
            }
        }
        //Slowly move the enemy to the second point. Also turn towards that point.
        if (close)
        {
            leave = true;
            distance = speed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, point2.position, distance);

            Vector3 place2 = new Vector3(point2.transform.position.x - this.transform.position.x, 0, point2.transform.position.z - this.transform.position.z);
            Quaternion rot2 = Quaternion.LookRotation(place2); //Tells the npc to literally look at that place.
            this.transform.rotation = rot2; //Rotate the gameobject to that place.
        }
        //If this enemy reaches the second point, despawn
        if (this.transform.position == point2.transform.position && leave)
        {
            var deadParticle = Instantiate(poofParticle, transform.position, transform.rotation);
            gameObject.SetActive(false);
            leave = false;
        }
    }
}
