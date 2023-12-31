using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is a level controller.
public class ScrapyardController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource bossMusic;
    bool musicFaded = false;
    bool bossMusicPlay = false;
    float fadeTime = 2f;
    bool fadedIn = false;
    bool fadedOut = false;
    bool musicPlay = false;

    public GameObject lastSpawn;
    public GameObject fireCol1;
    public GameObject fireCol2;
    public GameObject firePar1;
    public GameObject firePar2;
    public GameObject fireSoundLast1;
    public GameObject fireSoundLast2;

    public AudioSource waterSound1;
    public AudioSource waterSound2;
    public AudioSource waterSound3;

    public AudioSource fireSound1;
    public AudioSource fireSound2;
    public AudioSource fireSound3;

    public Image black;
    public Image textBack;
    public Text weaponUse;
    public Text button;
    bool gunPicked = false;
    bool gun3;
    float timerToFade = 6f;

    //For the cutscenes
    public GameObject playerCamera;
    public GameObject cameraGate2;
    public GameObject cameraGate3;
    public GameObject cameraWater;
    bool waterflowing1 = false;
    bool waterflowing2 = false;
    bool waterflowing3 = false;
    bool watercamerastop1 = false;
    bool watercamerastop2 = false;
    bool watercamerastop3 = false;
    bool gateopen2 = true;
    bool gateopen3 = true;
    bool retMainCam = false;
    float timerValveTurn = 1f;
    float timerCameraWater = 1.5f;
    float timerCameraGate = 2f;
    float timerCameraWaterLast = 4.5f;

    public GameObject player;
    public GameObject respawnPad;
    public GameObject bossHitBox;
    public GameObject bossHitBoxBlocker;
    public GameObject boss;
    public GameObject bossDestroyed;
    float bossCurHealth;
    float bossMaxHealth;
    public Image bossUI;
    public Image boss1UI;
    public Image boss2UI;
    public Image boss3UI;
    public Image bossHealthUI;
    public Image bossHealthBackUI;
    bool bossDead;
    bool respawning;
    bool alreadyDead;
    bool died = false;

    public GameObject checkpoint;

    public GameObject gate1;
    public GameObject gate2;
    public GameObject gate3;
    public GameObject gateEnd;

    Animator animGate1;
    Animator animGate2;
    Animator animGate3;
    Animator animGateEnd;

    public GameObject pipe1;
    public GameObject pipe2;
    public GameObject pipe3;

    public GameObject pipeCollider1;
    public GameObject pipeCollider2;
    public GameObject pipeCollider3;

    Animator animPipe1;
    Animator animPipe2;
    Animator animPipe3;

    bool turn1;
    bool turn2;
    bool turn3;

    bool turned1 = false;
    bool turned2 = false;
    bool turned3 = false;

    bool collided;
    bool playerDead;

    public GameObject smoke1;
    public GameObject smoke2;
    public GameObject smoke3;
    public GameObject water1;
    public GameObject water2;
    public GameObject water3;

    bool extinguish = false;
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    public GameObject fire4;
    public GameObject fire5;
    public GameObject fire6;
    public GameObject fire7;
    public GameObject fire8;
    public GameObject fire9;

    float timerToExtinguish = 3f;

    // Start is called before the first frame update
    void Start()
    {
        black.CrossFadeAlpha(0, 1, false);

        animGate1 = gate1.GetComponentInChildren<Animator>();
        animGate2 = gate2.GetComponentInChildren<Animator>();
        animGate3 = gate3.GetComponentInChildren<Animator>();
        animGateEnd = gateEnd.GetComponentInChildren<Animator>();

        animPipe1 = pipe1.GetComponentInChildren<Animator>();
        animPipe2 = pipe2.GetComponentInChildren<Animator>();
        animPipe3 = pipe3.GetComponentInChildren<Animator>();

        bossUI.canvasRenderer.SetAlpha(0.0f);
        boss1UI.canvasRenderer.SetAlpha(0.0f);
        boss2UI.canvasRenderer.SetAlpha(0.0f);
        boss3UI.canvasRenderer.SetAlpha(0.0f);
        bossHealthUI.canvasRenderer.SetAlpha(0.0f);
        bossHealthBackUI.canvasRenderer.SetAlpha(0.0f);

        textBack.canvasRenderer.SetAlpha(0.0f);
        weaponUse.canvasRenderer.SetAlpha(0.0f);
        button.canvasRenderer.SetAlpha(0.0f);

        animGate1.SetBool("Open", true);
    }

    // Update is called once per frame
    void Update()
    {
        gun3 = player.GetComponent<WeaponSwitch>().Gun3;

        playerDead = player.GetComponent<PlayerStats>().isDead;
        respawning = respawnPad.GetComponentInChildren<RespawnDeath>().respawning;
        bossDead = bossHitBox.GetComponent<EnemyStats>().isDead;
        bossCurHealth = bossHitBox.GetComponent<EnemyStats>().curHealth;
        bossMaxHealth = bossHitBox.GetComponent<EnemyStats>().maxHealth;

        collided = checkpoint.GetComponent<ControllerCollider>().collided;

        turn1 = pipeCollider1.GetComponent<PipeCollider>().turn;
        turn2 = pipeCollider2.GetComponent<PipeCollider>().turn;
        turn3 = pipeCollider3.GetComponent<PipeCollider>().turn;

        //For the boss' health bar
        bossHealthUI.rectTransform.sizeDelta = new Vector2((float)bossCurHealth / bossMaxHealth * 100, 123.7f);


        if (turn1 && !turned1)
        {
            animPipe1.SetTrigger("Turn");

            //Waits for the valve
            if (!waterflowing1)
            {
                timerValveTurn -= Time.deltaTime;
                if (timerValveTurn <= 0.0f)
                {
                    player.GetComponent<Move>().enabled = false; //The player won't be able to move during the cutscene
                    player.GetComponent<PlayerStats>().enabled = false; //The player won't take any damage
                    playerCamera.SetActive(false);
                    cameraWater.SetActive(true);
                    waterSound1.Play();
                    water1.GetComponent<ParticleSystem>().Play();
                    smoke1.GetComponent<ParticleSystem>().Play();
                    waterflowing1 = true;
                    timerValveTurn = 1f;
                }
            }
            //Camera waits
            if (waterflowing1 && !watercamerastop1)
            {
                timerCameraWater -= Time.deltaTime;
                if (timerCameraWater <= 0.0f)
                {
                    watercamerastop1 = true;
                    gateopen2 = false;
                    timerCameraWater = 1.5f;
                }
            }
            //Gate opens
            if (!gateopen2)
            {
                cameraWater.SetActive(false);
                cameraGate2.SetActive(true);
                animGate2.SetBool("Open", true);

                timerCameraGate -= Time.deltaTime;
                if (timerCameraGate <= 0.0f)
                {
                    cameraGate2.SetActive(false);
                    gateopen2 = true;
                    turned1 = true;

                    //To resume the game
                    retMainCam = true;

                    timerCameraGate = 2f;
                }
            }
        }

        if (turn2 && !turned2)
        {
            animPipe2.SetTrigger("Turn");

            //Waits for the valve
            if (!waterflowing2)
            {
                timerValveTurn -= Time.deltaTime;
                if (timerValveTurn <= 0.0f)
                {
                    player.GetComponent<Move>().enabled = false; //The player won't be able to move during the cutscene
                    player.GetComponent<PlayerStats>().enabled = false; //The player won't take any damage
                    playerCamera.SetActive(false);
                    cameraWater.SetActive(true);
                    waterSound2.Play();
                    water2.GetComponent<ParticleSystem>().Play();
                    smoke2.GetComponent<ParticleSystem>().Play();
                    waterflowing2 = true;
                    timerValveTurn = 1f;
                }
            }
            //Camera waits
            if (waterflowing2 && !watercamerastop2)
            {
                timerCameraWater -= Time.deltaTime;
                if (timerCameraWater <= 0.0f)
                {
                    watercamerastop2 = true;
                    gateopen3 = false;
                    timerCameraWater = 1.5f;
                }
            }
            //Gate opens
            if (!gateopen3)
            {
                cameraWater.SetActive(false);
                cameraGate3.SetActive(true);
                animGate3.SetBool("Open", true);

                timerCameraGate -= Time.deltaTime;
                if (timerCameraGate <= 0.0f)
                {
                    cameraGate3.SetActive(false);
                    gateopen3 = true;
                    turned2 = true;

                    //To resume the game
                    retMainCam = true;

                    timerCameraGate = 2f;
                }
            }
        }
        //Fires will be extinguished and the boss' hitbox will be active.
        if(turn3 && !turned3)
        {
            lastSpawn.SetActive(true);
            firePar1.GetComponent<ParticleSystem>().Play();
            firePar2.GetComponent<ParticleSystem>().Play();
            fireSoundLast1.SetActive(true);
            fireSoundLast2.SetActive(true);
            fireCol1.SetActive(true);
            fireCol2.SetActive(true);

            animPipe3.SetTrigger("Turn");

            //Waits for the valve
            if (!waterflowing3)
            {
                timerValveTurn -= Time.deltaTime;
                if (timerValveTurn <= 0.0f)
                {
                    player.GetComponent<Move>().enabled = false; //The player won't be able to move during the cutscene
                    player.GetComponent<PlayerStats>().enabled = false; //The player won't take any damage
                    playerCamera.SetActive(false);
                    cameraWater.SetActive(true);
                    waterSound3.Play();
                    water3.GetComponent<ParticleSystem>().Play();
                    smoke3.GetComponent<ParticleSystem>().Play();
                    waterflowing3 = true;
                    extinguish = true;
                }
            }

            //Extinguishing the fires
            if(extinguish)
            {
                timerToExtinguish -= Time.deltaTime;
                if (timerToExtinguish <= 0.0f)
                {
                    bossHitBoxBlocker.SetActive(false);

                    fireSound1.Stop();
                    fireSound2.Stop();
                    fireSound3.Stop();

                    smoke1.GetComponent<ParticleSystem>().Stop();
                    smoke2.GetComponent<ParticleSystem>().Stop();
                    smoke3.GetComponent<ParticleSystem>().Stop();
                    fire1.GetComponent<ParticleSystem>().Stop();
                    fire2.GetComponent<ParticleSystem>().Stop();
                    fire3.GetComponent<ParticleSystem>().Stop();
                    fire4.GetComponent<ParticleSystem>().Stop();
                    fire5.GetComponent<ParticleSystem>().Stop();
                    fire6.GetComponent<ParticleSystem>().Stop();
                    fire7.GetComponent<ParticleSystem>().Stop();
                    fire8.GetComponent<ParticleSystem>().Stop();
                    fire9.GetComponent<ParticleSystem>().Stop();

                    extinguish = false;
                }
            }
            //Camera waits
            if (waterflowing3 && !watercamerastop3)
            {
                timerCameraWaterLast -= Time.deltaTime;
                if (timerCameraWaterLast <= 0.0f)
                {
                    watercamerastop3 = true;
                }
            }
            //Returns to the player
            if (watercamerastop3)
            {
                cameraWater.SetActive(false);
                turned3 = true;
                retMainCam = true;
            }
        }
        if(bossDead)
        {
            if (!fadedIn)
            {
                if(!musicPlay)
                {
                    music.Play();
                    musicPlay = true;
                }
                MusicFadeIn();
            }

            animGateEnd.SetBool("Open", true);
            bossDestroyed.SetActive(true);
        }

        //To activate the boss
        if(collided && !alreadyDead && boss.activeInHierarchy)
        {
            if (!fadedOut)
            {
                MusicFadeOut();
            }

            bossUI.CrossFadeAlpha(1, 1, false);
            boss2UI.CrossFadeAlpha(1, 1, false);
            bossHealthUI.CrossFadeAlpha(1, 1, false);
            bossHealthBackUI.CrossFadeAlpha(1, 1, false);

            if(playerDead)
            {
                died = true;
                bossMusicPlay = false;
            }
        }
        else
        {
            bossUI.CrossFadeAlpha(0, 1, false);
            boss2UI.CrossFadeAlpha(0, 1, false);
            bossHealthUI.CrossFadeAlpha(0, 1, false);
            bossHealthBackUI.CrossFadeAlpha(0, 1, false);
        }

        if (died)
        {
            bossMusic.Stop();

            bossUI.CrossFadeAlpha(0, 0.5f, false);
            boss2UI.CrossFadeAlpha(0, 0.5f, false);
            bossHealthUI.CrossFadeAlpha(0, 0.5f, false);
            bossHealthBackUI.CrossFadeAlpha(0, 0.5f, false);
            alreadyDead = true;
            died = false;
        }

        if (collided && boss.activeInHierarchy && respawning)
        {
            if(!bossMusicPlay)
            {
                bossMusic.Play();
                bossMusicPlay = true;
            }

            bossUI.CrossFadeAlpha(1, 0.5f, false);
            boss2UI.CrossFadeAlpha(1, 0.5f, false);
            bossHealthUI.CrossFadeAlpha(1, 0.5f, false);
            bossHealthBackUI.CrossFadeAlpha(1, 0.5f, false);
            alreadyDead = false;
        }

        if(playerDead && !collided)
        {
            music.Stop();
        }

        if(respawning && !collided)
        {
            music.Play();
        }

        //Returns to the player
        if(retMainCam)
        {
            playerCamera.SetActive(true);
            player.GetComponent<Move>().enabled = true;
            player.GetComponent<PlayerStats>().enabled = true;
            retMainCam = false;
        }

        if (gun3 && !gunPicked)
        {
            textBack.CrossFadeAlpha(1, 0.3f, false);
            weaponUse.CrossFadeAlpha(1, 0.3f, false);
            button.CrossFadeAlpha(1, 0.3f, false);

            timerToFade -= Time.deltaTime;
            if (timerToFade <= 0)
            {
                textBack.CrossFadeAlpha(0, 0.3f, false);
                weaponUse.CrossFadeAlpha(0, 0.3f, false);
                button.CrossFadeAlpha(0, 0.3f, false);
                gunPicked = true;
            }
        }

    }

    void MusicFadeIn()
    {
        bossMusic.volume -= Time.deltaTime / fadeTime;
        if (bossMusic.volume <= 0 && musicFaded)
        {
            music.volume += Time.deltaTime / fadeTime;
            if (music.volume >= 0.8f)
            {
                musicFaded = false;
                fadedIn = true;
            }
        }
    }

    void MusicFadeOut()
    {
        music.volume -= Time.deltaTime / fadeTime;
        if (music.volume <= 0 && !musicFaded)
        {
            bossMusic.Play();
            music.Stop();
            musicFaded = true;
            fadedOut = true;
        }
    }
}
