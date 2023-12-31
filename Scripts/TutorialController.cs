using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is a level controller.
public class TutorialController : MonoBehaviour
{
    public Image black;
    public GameObject deathPoint;
    public GameObject deathPoint2;

    int part;
    int state;
    bool gunPicked = false;
    bool gun1;
    bool sp;
    int weaponType;
    bool botDead1 = false;
    bool botDead2 = false;
    float curSuperPower;
    float timerToText = 6f;
    public Image TextBackground;
    public Text Move;
    public Text MoveAdd;
    public Text JumpC;
    public Text JumpAdd;
    public Text MoveTo1;

    public Text Shift;
    public Text ShiftAdd;
    public Text Stamina;
    public Text StaminaAdd;
    public Text Stamina2;
    public Text Stamina2Add;

    public Text Punch;
    public Text PunchAdd;
    public Text BotDead;
    public Text WeaponPick;
    public Text WeaponPickAdd;
    public Text WeaponPickPress2;
    public Text WeaponPickPress2Add;
    public Text WeaponPick2;

    public Text WeaponUse;
    public Text WeaponUseAdd;
    public Text WeaponUse2;
    public Text WeaponUse2Add;
    public Text WeaponUse3;
    public Text WeaponUse4;
    public Text WeaponUse5;
    public Text WeaponUse5Add;
    public Text WeaponUse6;
    public Text WeaponUse6Add;
    public Text MoveTo2;

    public Text SuperUse;
    public Text SuperUseAdd;
    public Text SuperUse2;
    public Text SuperUse2Add;
    public Text SuperUse3;
    public Text SuperUse3Add;
    public Text SuperUse4;
    public Text SuperUse4Add;
    public Text SuperUse5;
    public Text MoveTo3;

    public Text checkpoint;
    public Text HealthUse;
    public Text HealthUseAdd;
    public Text HealthUse2;
    public Text HealthUse2Add;
    public Text DeathUse;
    public Text DeathUse2;
    public Text DeathUse3;
    public Text DeathUse4;
    public Text DeathUse5;
    public Text MoveToEnd;
    public Text MoveToEndAdd;

    public Text Flashlight;
    public Text FlashlightAdd;
    public Text EndText;

    public GameObject player;
    public GameObject respawnPad;

    public GameObject clockcopter1;
    public GameObject clockcopter2;
    public GameObject clockbot1;
    public GameObject clockbot2;

    bool respawning;
    bool alreadyDead;
    bool died = false;

    public GameObject switchCol;
    public GameObject col1;
    public GameObject col2;
    public GameObject col3;
    public GameObject col4;
    public GameObject col5;
    public GameObject col6;
    public GameObject col7;

    public GameObject MoveUp;
    public GameObject MoveUp2;
    public GameObject MoveUp3;
    public GameObject gateEnd;

    Animator animMove;
    Animator animMove2;
    Animator animMove3;
    Animator animGateEnd;

    bool collided1;
    bool collided2;
    bool collided3;
    bool collided4;
    bool collided5;
    bool collided6;
    bool collided7;
    bool playerDead;

    // Start is called before the first frame update
    void Start()
    {
        black.CrossFadeAlpha(0, 0.3f, false);

        part = 1;
        state = 1;
        animMove = MoveUp.GetComponent<Animator>();
        animMove2 = MoveUp2.GetComponent<Animator>();
        animMove3 = MoveUp3.GetComponent<Animator>();
        animGateEnd = gateEnd.GetComponentInChildren<Animator>();

        TextBackground.canvasRenderer.SetAlpha(1f);
        Move.canvasRenderer.SetAlpha(1f);
        MoveAdd.canvasRenderer.SetAlpha(1f);
        JumpC.canvasRenderer.SetAlpha(0f);
        JumpAdd.canvasRenderer.SetAlpha(0f);
        MoveTo1.canvasRenderer.SetAlpha(0f);

        Shift.canvasRenderer.SetAlpha(0f);
        ShiftAdd.canvasRenderer.SetAlpha(0f);
        Stamina.canvasRenderer.SetAlpha(0f);
        StaminaAdd.canvasRenderer.SetAlpha(0f);
        Stamina2.canvasRenderer.SetAlpha(0f);
        Stamina2Add.canvasRenderer.SetAlpha(0f);

        Punch.canvasRenderer.SetAlpha(0f);
        PunchAdd.canvasRenderer.SetAlpha(0f);
        BotDead.canvasRenderer.SetAlpha(0f);
        WeaponPick.canvasRenderer.SetAlpha(0f);
        WeaponPickAdd.canvasRenderer.SetAlpha(0f);
        WeaponPickPress2.canvasRenderer.SetAlpha(0f);
        WeaponPickPress2Add.canvasRenderer.SetAlpha(0f);
        WeaponPick2.canvasRenderer.SetAlpha(0f);

        WeaponUse.canvasRenderer.SetAlpha(0f);
        WeaponUseAdd.canvasRenderer.SetAlpha(0f);
        WeaponUse2.canvasRenderer.SetAlpha(0f);
        WeaponUse2Add.canvasRenderer.SetAlpha(0f);
        WeaponUse3.canvasRenderer.SetAlpha(0f);
        WeaponUse4.canvasRenderer.SetAlpha(0f);
        WeaponUse5.canvasRenderer.SetAlpha(0f);
        WeaponUse5Add.canvasRenderer.SetAlpha(0f);
        WeaponUse6.canvasRenderer.SetAlpha(0f);
        WeaponUse6Add.canvasRenderer.SetAlpha(0f);
        MoveTo2.canvasRenderer.SetAlpha(0f);

        SuperUse.canvasRenderer.SetAlpha(0f);
        SuperUseAdd.canvasRenderer.SetAlpha(0f);
        SuperUse2.canvasRenderer.SetAlpha(0f);
        SuperUse2Add.canvasRenderer.SetAlpha(0f);
        SuperUse3.canvasRenderer.SetAlpha(0f);
        SuperUse3Add.canvasRenderer.SetAlpha(0f);
        SuperUse4.canvasRenderer.SetAlpha(0f);
        SuperUse4Add.canvasRenderer.SetAlpha(0f);
        SuperUse5.canvasRenderer.SetAlpha(0f);
        MoveTo3.canvasRenderer.SetAlpha(0f);

        checkpoint.canvasRenderer.SetAlpha(0f);
        HealthUse.canvasRenderer.SetAlpha(0f);
        HealthUseAdd.canvasRenderer.SetAlpha(0f);
        HealthUse2.canvasRenderer.SetAlpha(0f);
        HealthUse2Add.canvasRenderer.SetAlpha(0f);
        DeathUse.canvasRenderer.SetAlpha(0f);
        DeathUse2.canvasRenderer.SetAlpha(0f);
        DeathUse3.canvasRenderer.SetAlpha(0f);
        DeathUse4.canvasRenderer.SetAlpha(0f);
        DeathUse5.canvasRenderer.SetAlpha(0f);
        MoveToEnd.canvasRenderer.SetAlpha(0f);
        MoveToEndAdd.canvasRenderer.SetAlpha(0f);

        Flashlight.canvasRenderer.SetAlpha(0f);
        FlashlightAdd.canvasRenderer.SetAlpha(0f);
        EndText.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        playerDead = player.GetComponent<PlayerStats>().isDead;
        curSuperPower = player.GetComponent<PlayerStats>().CurSuperPower;
        respawning = respawnPad.GetComponentInChildren<RespawnDeath>().respawning;
        gun1 = player.GetComponent<WeaponSwitch>().Gun1;
        weaponType = player.GetComponent<WeaponSwitch>().weaponType;
        collided1 = col1.GetComponent<ControllerCollider>().collided;
        collided2 = col2.GetComponent<ControllerCollider>().collided;
        collided3 = col3.GetComponent<ControllerCollider>().collided;
        collided4 = col4.GetComponent<ControllerCollider>().collided;
        collided5 = col5.GetComponent<ControllerCollider>().collided;
        collided6 = col6.GetComponent<ControllerCollider>().collided;
        collided7 = col7.GetComponent<ControllerCollider>().collided;

        if (part == 1)
        {
            Movement();
        }
        if(part == 2)
        {
            Combat();
        }
        if(part == 3)
        {
            Super();
        }
        if(part == 4)
        {
            Health();
        }
        if(part == 5)
        {
            End();
        }
    }

    void Movement()
    {
        if(state == 1)
        {
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                Move.CrossFadeAlpha(0, 0.3f, false);
                MoveAdd.CrossFadeAlpha(0, 0.3f, false);
                timerToText = 6f;
                state = 2;
            }
        }
        if(state == 2)
        {
            JumpC.CrossFadeAlpha(1, 0.3f, false);
            JumpAdd.CrossFadeAlpha(1, 0.3f, false);
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                JumpC.CrossFadeAlpha(0, 0.3f, false);
                JumpAdd.CrossFadeAlpha(0, 0.3f, false);
                timerToText = 6f;
                state = 3;
            }
        }
        if(state == 3)
        {
            MoveTo1.CrossFadeAlpha(1, 0.3f, false);
        }

        if(collided1)
        {
            state = 4;
            col1.GetComponent<ControllerCollider>().collided = false;
        }

        if(state == 4)
        {
            MoveTo1.CrossFadeAlpha(0, 0.3f, false);
            Move.CrossFadeAlpha(0, 0.3f, false);
            MoveAdd.CrossFadeAlpha(0, 0.3f, false);
            JumpC.CrossFadeAlpha(0, 0.3f, false);
            JumpAdd.CrossFadeAlpha(0, 0.3f, false);
            Shift.CrossFadeAlpha(1, 0.3f, false);
            ShiftAdd.CrossFadeAlpha(1, 0.3f, false);
            player.GetComponent<Move>().enabled = false;

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                Shift.CrossFadeAlpha(0, 0.3f, false);
                ShiftAdd.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 5;
            }
        }

        if(state == 5)
        {
            Stamina.CrossFadeAlpha(1, 0.3f, false);
            StaminaAdd.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                Stamina.CrossFadeAlpha(0, 0.3f, false);
                StaminaAdd.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 6;
            }
        }

        if(state == 6)
        {
            player.GetComponent<Move>().enabled = true;
            Stamina2.CrossFadeAlpha(1, 0.3f, false);
            Stamina2Add.CrossFadeAlpha(1, 0.3f, false);
        }

        if(collided2)
        {
            state = 7;
            part = 2;
            col2.GetComponent<ControllerCollider>().collided = false;
        }
    }

    void Combat()
    {
        if (state == 7)
        {
            Stamina2.CrossFadeAlpha(0, 0.3f, false);
            Stamina2Add.CrossFadeAlpha(0, 0.3f, false);
            clockcopter1.SetActive(true);
            state = 8;
        }
        if (state == 8 && clockbot1.activeInHierarchy)
        {
            Punch.CrossFadeAlpha(1, 0.3f, false);
            PunchAdd.CrossFadeAlpha(1, 0.3f, false);
            botDead1 = true;
        }

        if (!clockbot1.activeInHierarchy && botDead1)
        {
            Punch.CrossFadeAlpha(0, 0.3f, false);
            PunchAdd.CrossFadeAlpha(0, 0.3f, false);
            state = 9;
            botDead1 = false;
        }
        if (state == 9)
        {
            player.GetComponent<Move>().enabled = false;
            BotDead.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                timerToText = 6f;
                state = 10;
            }
        }
        if (state == 10)
        {
            player.GetComponent<Move>().enabled = true;
            BotDead.CrossFadeAlpha(0, 0.3f, false);
            
            WeaponPick.CrossFadeAlpha(1, 0.3f, false);
            WeaponPickAdd.CrossFadeAlpha(1, 0.3f, false);
        }
        if (gun1 && !gunPicked)
        {
            WeaponPick.CrossFadeAlpha(0, 0.3f, false);
            WeaponPickAdd.CrossFadeAlpha(0, 0.3f, false);
            gunPicked = true;
            state = 11;
        }
        if (state == 11)
        {
            WeaponPickPress2.CrossFadeAlpha(1, 0.3f, false);
            WeaponPickPress2Add.CrossFadeAlpha(1, 0.3f, false);
            state = 12;
        }
        if (state == 12 && weaponType == 2)
        {
            WeaponPickPress2.CrossFadeAlpha(0, 0.3f, false);
            WeaponPickPress2Add.CrossFadeAlpha(0, 0.3f, false);
            WeaponPick2.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                WeaponPick2.CrossFadeAlpha(0, 0.3f, false);
                WeaponUse.CrossFadeAlpha(1, 0.3f, false);
                WeaponUseAdd.CrossFadeAlpha(1, 0.3f, false);

                timerToText = 6f;
                state = 13;
            }
        }
        if(state == 13)
        {
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                WeaponUse.CrossFadeAlpha(0, 0.3f, false);
                WeaponUseAdd.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 14;
            }
        }
        if (state == 14)
        {
            WeaponUse2.CrossFadeAlpha(1, 0.3f, false);
            WeaponUse2Add.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                WeaponUse2.CrossFadeAlpha(0, 0.3f, false);
                WeaponUse2Add.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 15;
            }
        }
        if (state == 15)
        {
            WeaponUse3.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                WeaponUse3.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 16;
            }
        }
        if (state == 16)
        {
            WeaponUse4.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                WeaponUse4.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 17;
            }
        }
        if (state == 17)
        {
            clockcopter2.SetActive(true);
            WeaponUse5.CrossFadeAlpha(1, 0.3f, false);
            WeaponUse5Add.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                WeaponUse5.CrossFadeAlpha(0, 0.3f, false);
                WeaponUse5Add.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 18;
            }
        }

        if(clockbot2.activeInHierarchy && state == 18)
        {
            WeaponUse6.CrossFadeAlpha(1, 0.3f, false);
            WeaponUse6Add.CrossFadeAlpha(1, 0.3f, false);
            botDead2 = true;
        }

        if (!clockbot2.activeInHierarchy && botDead2)
        {
            WeaponUse6.CrossFadeAlpha(0, 0.3f, false);
            WeaponUse6Add.CrossFadeAlpha(0, 0.3f, false);
            state = 19;
            botDead2 = false;
        }

        if(state == 19)
        {
            MoveTo2.CrossFadeAlpha(1, 0.3f, false);
            animMove.SetBool("MoveUp", true);
            col3.SetActive(true);
        }
        if(collided3)
        {
            state = 20;
            part = 3;
            col3.GetComponent<ControllerCollider>().collided = false;
        }
    }

    void Super()
    {
        if(state == 20)
        {
            player.GetComponent<Move>().enabled = false;
            MoveTo2.CrossFadeAlpha(0, 0.3f, false);
            SuperUse.CrossFadeAlpha(1, 0.3f, false);
            SuperUseAdd.CrossFadeAlpha(1, 0.3f, false);

            state = 21;
        }
        if(state == 21)
        {
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                SuperUse.CrossFadeAlpha(0, 0.3f, false);
                SuperUseAdd.CrossFadeAlpha(0, 0.3f, false);
                timerToText = 6f;
                state = 22;
            }
        }
        if (state == 22)
        {
            SuperUse2.CrossFadeAlpha(1, 0.3f, false);
            SuperUse2Add.CrossFadeAlpha(1, 0.3f, false);
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                SuperUse2.CrossFadeAlpha(0, 0.3f, false);
                SuperUse2Add.CrossFadeAlpha(0, 0.3f, false);
                timerToText = 6f;
                state = 23;
            }
        }
        if(state == 23)
        {
            player.GetComponent<Move>().enabled = true;
            SuperUse3.CrossFadeAlpha(1, 0.3f, false);
            SuperUse3Add.CrossFadeAlpha(1, 0.3f, false);
            state = 24;
        }
        if(state == 24 && curSuperPower >= 100)
        {
            SuperUse3.CrossFadeAlpha(0, 0.3f, false);
            SuperUse3Add.CrossFadeAlpha(0, 0.3f, false);
            SuperUse4.CrossFadeAlpha(1, 0.3f, false);
            SuperUse4Add.CrossFadeAlpha(1, 0.3f, false);
            sp = true;
        }

        if(sp)
        {
            timerToText -= Time.deltaTime;
            if (timerToText <= 0)
            {
                SuperUse4.CrossFadeAlpha(0, 0.3f, false);
                SuperUse4Add.CrossFadeAlpha(0, 0.3f, false);
                timerToText = 6f;
                state = 25;
                sp = false;
            }
        }
        if (state == 25)
        {
            SuperUse5.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                SuperUse5.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 26;
            }
        }
        if (state == 26)
        {
            MoveTo3.CrossFadeAlpha(1, 0.3f, false);
            animMove2.SetBool("MoveUp", true);
            col4.SetActive(true);
        }
        if (collided4)
        {
            state = 27;
            part = 4;
            col4.GetComponent<ControllerCollider>().collided = false;
            deathPoint.transform.position = deathPoint2.transform.position;
        }
    }

    void Health()
    {
        if(state == 27)
        {
            player.GetComponent<Move>().enabled = false;
            MoveTo3.CrossFadeAlpha(0, 0.3f, false);
            state = 28;
        }
        if (state == 28)
        {
            checkpoint.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                checkpoint.CrossFadeAlpha(0, 0.3f, false);
                timerToText = 6f;
                state = 29;
            }
        }
        if (state == 29)
        {
            HealthUse.CrossFadeAlpha(1, 0.3f, false);
            HealthUseAdd.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                HealthUse.CrossFadeAlpha(0, 0.3f, false);
                HealthUseAdd.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 30;
            }
        }
        if (state == 30)
        {
            HealthUse2.CrossFadeAlpha(1, 0.3f, false);
            HealthUse2Add.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                HealthUse2.CrossFadeAlpha(0, 0.3f, false);
                HealthUse2Add.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 31;
            }
        }
        if (state == 31)
        {
            DeathUse.CrossFadeAlpha(1, 0.3f, false);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                DeathUse.CrossFadeAlpha(0, 0.3f, false);

                timerToText = 6f;
                state = 32;
            }
        }
        if (state == 32)
        {
            player.GetComponent<Move>().enabled = true;
            DeathUse2.CrossFadeAlpha(1, 0.3f, false);
            state = 33;
        }
        if(state == 33 && collided5)
        {
            DeathUse2.CrossFadeAlpha(0, 0.3f, false);
            player.GetComponent<Move>().enabled = false;
            DeathUse3.CrossFadeAlpha(1, 0.3f, false);
            state = 34;
            col5.GetComponent<ControllerCollider>().collided = false;
            switchCol.SetActive(true);
        }
        if (state == 34)
        {
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                DeathUse3.CrossFadeAlpha(0, 0.3f, false);
                DeathUse4.CrossFadeAlpha(1, 0.3f, false);
                timerToText = 6f;
                state = 35;
            }
        }
        if (state == 35)
        {
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                DeathUse4.CrossFadeAlpha(0, 0.3f, false);
                DeathUse5.CrossFadeAlpha(1, 0.3f, false);
                player.GetComponent<Move>().enabled = true;
                timerToText = 6f;
                state = 36;
            }
        }
        if(state == 36 && respawning)
        {
            DeathUse3.CrossFadeAlpha(0, 0.3f, false);
            DeathUse4.CrossFadeAlpha(0, 0.3f, false);
            DeathUse5.CrossFadeAlpha(0, 0.3f, false);
            state = 37;
        }
        if(state == 37)
        {
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                MoveToEnd.CrossFadeAlpha(1, 0.3f, false);
                MoveToEndAdd.CrossFadeAlpha(1, 0.3f, false);
                timerToText = 6f;
                state = 38;
            }
        }
        if(state == 38 && !switchCol.activeInHierarchy)
        {
            animMove3.SetBool("MoveUp", true);
            state = 39;
            part = 5;
        }
    }

    void End()
    {
        if(state == 39)
        {
            MoveToEnd.CrossFadeAlpha(0, 0.3f, false);
            MoveToEndAdd.CrossFadeAlpha(0, 0.3f, false);
            state = 40;
        }
        if(state == 40 && collided6)
        {
            Flashlight.CrossFadeAlpha(1, 0.3f, false);
            FlashlightAdd.CrossFadeAlpha(1, 0.3f, false);
            col6.GetComponent<ControllerCollider>().collided = false;
            state = 41;
        }
        if(state == 41)
        {
            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                Flashlight.CrossFadeAlpha(0, 0.3f, false);
                FlashlightAdd.CrossFadeAlpha(0, 0.3f, false);
                timerToText = 15f;
                state = 42;
            }
        }
        if (state == 42 && collided7)
        {
            Flashlight.CrossFadeAlpha(0, 0.3f, false);
            FlashlightAdd.CrossFadeAlpha(0, 0.3f, false);
            EndText.CrossFadeAlpha(1, 0.3f, false);
            col7.GetComponent<ControllerCollider>().collided = false;
            animGateEnd.SetBool("Open", true);

            timerToText -= Time.deltaTime;
            if (timerToText <= 0.0f)
            {
                EndText.CrossFadeAlpha(0, 0.3f, false);
                state = 0;
            }
        }
    }
}
