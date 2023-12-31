using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public AudioSource jumpAudio;
    public AudioSource crouchDownAudio;
    public AudioSource crouchUpAudio;

    float speed;
    float walk = 20;
    float run = 35;
    float crouch = 10;
    public float jump;
    //Different gravities, one for the ground, the other is for the air (to solve slope bump problems)
    public float gravity;
    public float gravityAir;
    float defaultHeight;

    public bool isRunning = false;
    public bool isCrouching = false;
    public bool isAir = false;

    Vector3 direction;
    CharacterController controller;
    Animator anim;
    PlayerStats playerStats;

    //For crouching
    Vector3 newCenter;
    public Transform crouchTransform;
    Vector3 handHeight;
    Vector3 defaultHandHeight;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        anim = GetComponentInChildren<Animator> (); //Find that component in the player's "children"

        //For crouching
        defaultHeight = controller.height;
        newCenter = controller.center;
        handHeight = crouchTransform.transform.localPosition;
        defaultHandHeight = crouchTransform.transform.localPosition;  
    }

    // Update is called once per frame
    void Update()
    {
        float rightleft = Input.GetAxis("Horizontal");   //Right and left, sideways
        float forback = Input.GetAxis("Vertical");      //Forward and backward

        isAir = true;
        isRunning = false;

        //If player is on the ground
        if (controller.isGrounded)
        {
            isAir = false;
            MoveFunc();
            //Gravity to pull down if the player is on the ground (solves slope bump)
            direction.y -= gravity;
        }
        //If player is in the air, play the falling animation
        if (isAir == true)
        {
            anim.SetBool("Air", true);
            speed = crouch;
            //Gravity for when the player is airborne. Multiplied by Time.deltaTime and 0.5 to solve frame related gravity issues.
            direction.y -= gravityAir * Time.deltaTime * 0.5f;
        }
        controller.Move(direction * Time.deltaTime); //Time.deltatime is updating according to time and not frame
    }

    void MoveFunc()
    {
        //Gets the isStaminaEnd bool from PlayerStats
        bool endStamina = gameObject.GetComponent<PlayerStats>().isStaminaEnd;

        float rightleft = Input.GetAxis("Horizontal");  
        float forback = Input.GetAxis("Vertical");

        //If the player is on the ground, play the landing animation (after falling)
        anim.SetBool("Air", false);

        direction = new Vector3(rightleft, 0, forback);
        direction = transform.TransformDirection(direction); //To make the player move where the camera is looking
        direction *= speed;

        //If shift is held  - run and play the sprint animation (cond = 2)
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            isRunning = true;

            anim.SetInteger("cond", 2);

            speed = run;
        }
        //If stamina is over, dont run
        if (endStamina)
        {
            isRunning = false;
        }
        //If shift is released, dont run
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        //If player is not running and not crouching, walk
        if(!isRunning && !isCrouching)
        {
            speed = walk;
        }
        //If the player is moving
        if (forback != 0 || rightleft != 0)
        {
            //If the player is not crouching and not running, play the walk animation (cond = 1)
            if (!isRunning && !isCrouching)
            {
                anim.SetInteger("cond", 1);
            }
            //If the player is crouching, play the crouch walk animation
            if (isCrouching)
            {
                anim.SetInteger("cond", 3);
            }
        }
        //If you are not moving, the condition is set to 0 which is the melee idle animation
        if (forback == 0 && rightleft == 0)
        {
            anim.SetInteger("cond", 0);
        }
        //If space is pressed - jump and play the jump animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpAudio.Play();
            isRunning = false;
            anim.SetTrigger("Jump");
            direction.y += jump;
        }
        //If left ctrl is pressed, crouch (reduce the height of the controller and move the hands down) and play the crouch animation, set speed to crouch
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isRunning)
        {
            crouchDownAudio.Play();
            Crouched();
        }
        //If left ctrl is released, stand up (increase the height of the controller and move the hands up) and play the standing up animation, set speed to walk
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouchUpAudio.Play();
            UnCrouch();
        }
    }
    //To stop crouching and check if there is enough distance above the player to uncrouch
    void UnCrouch()
    {
        var startPosition = transform.position + new Vector3(0, 2f - (defaultHeight * 0.5f), 0);
        var length = (defaultHeight - 2f);
        if (Physics.Raycast(startPosition, Vector3.up, length))
        {
            Crouched();
        }
        else
        {
            anim.SetTrigger("MelCrouchUp");
            isCrouching = false;
            controller.height = defaultHeight;
            newCenter.y = 0f;
            controller.center = newCenter;
            handHeight.y = defaultHandHeight.y;
            crouchTransform.transform.localPosition = handHeight;
        }
    }
    //To stay crouched
    void Crouched()
    {
        anim.SetTrigger("MelCrouchDown");
        speed = crouch;
        isCrouching = true;
        controller.height = 2f;
        newCenter.y = -1f;
        controller.center = newCenter;
        handHeight.y = -0.5f;
        crouchTransform.transform.localPosition = handHeight;
    }
}
