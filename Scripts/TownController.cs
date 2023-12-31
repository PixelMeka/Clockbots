using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is a level controller.
public class TownController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource bossMusic;
    bool musicFaded = false;
    float fadeTime = 5f;
    bool fadedOut = false;
    bool fadedIn = false;
    bool musicPlay = false;

    public Image black;
    public Image textBack;
    public Text weaponUse;
    public Text button;
    bool gunPicked = false;
    bool gun2;
    float timerToFade = 6f;

    public GameObject player;
    public GameObject respawnPad;
    public GameObject firstSpawn;
    public GameObject secondSpawn;
    public GameObject bossSpawn;
    public GameObject lastSpawn;
    public GameObject clockcopter_Boss;
    public GameObject boss;
    public GameObject clockcopter_prop;
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

    public GameObject firstCheckpoint;
    public GameObject secondCheckpoint;
    public GameObject gateOpener;
    public GameObject gateCloser;
    public GameObject bossCheckpoint;
    public GameObject bossCheckpointEnd;

    public GameObject gateBossStart;
    public GameObject gateBossEnd;
    public GameObject gateEnd;

    Animator animGate1;
    Animator animGate2;
    Animator animGateEnd;

    bool collided1;
    bool collided2;
    bool collided3;
    bool collided4;
    bool collided5;
    bool collided6;
    bool playerDead;

    // Start is called before the first frame update
    void Start()
    {
        black.CrossFadeAlpha(0, 1, false);

        firstSpawn.SetActive(false);
        secondSpawn.SetActive(false);
        lastSpawn.SetActive(false);
        clockcopter_Boss.SetActive(false);

        animGate1 = gateBossStart.GetComponentInChildren<Animator>();
        animGate2 = gateBossEnd.GetComponentInChildren<Animator>();
        animGateEnd = gateEnd.GetComponentInChildren<Animator>();

        bossUI.canvasRenderer.SetAlpha(0.0f);
        boss1UI.canvasRenderer.SetAlpha(0.0f);
        boss2UI.canvasRenderer.SetAlpha(0.0f);
        boss3UI.canvasRenderer.SetAlpha(0.0f);
        bossHealthUI.canvasRenderer.SetAlpha(0.0f);
        bossHealthBackUI.canvasRenderer.SetAlpha(0.0f);

        textBack.canvasRenderer.SetAlpha(0.0f);
        weaponUse.canvasRenderer.SetAlpha(0.0f);
        button.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        gun2 = player.GetComponent<WeaponSwitch>().Gun2;

        playerDead = player.GetComponent<PlayerStats>().isDead;
        respawning = respawnPad.GetComponentInChildren<RespawnDeath>().respawning;
        collided1 = firstCheckpoint.GetComponent<ControllerCollider>().collided;
        collided2 = secondCheckpoint.GetComponent<ControllerCollider>().collided;
        collided3 = bossCheckpoint.GetComponent<ControllerCollider>().collided;
        collided4 = gateCloser.GetComponent<ControllerCollider>().collided;
        collided5 = bossCheckpointEnd.GetComponent<ControllerCollider>().collided;
        collided6 = gateOpener.GetComponent<ControllerCollider>().collided;
        bossDead = boss.GetComponent<EnemyStats>().isDead;
        bossCurHealth = boss.GetComponent<EnemyStats>().curHealth;
        bossMaxHealth = boss.GetComponent<EnemyStats>().maxHealth;
        //For the boss' health bar
        bossHealthUI.rectTransform.sizeDelta = new Vector2((float)bossCurHealth / bossMaxHealth * 100, 123.7f);

        if (collided1)
        {
            firstSpawn.SetActive(true);
        }
        if (collided2)
        {
            secondSpawn.SetActive(true);
        }
        if (collided6)
        {
            animGate1.SetBool("Open", true);
            gateOpener.GetComponent<ControllerCollider>().collided = false;
        }
        if (collided3)
        {
            clockcopter_Boss.SetActive(true);
        }
        if(collided4)
        {
            animGate1.SetBool("Open", false);
            gateCloser.GetComponent<ControllerCollider>().collided = false;
        }
        if(bossDead)
        {
            if(!fadedIn)
            {
                if (!musicPlay)
                {
                    music.Play();
                    musicPlay = true;
                }
                MusicFadeIn();
            }

            animGate1.SetBool("Open", true);
            animGate2.SetBool("Open", true);
        }
        if(collided5)
        {
            lastSpawn.SetActive(true);
            animGateEnd.SetBool("Open", true);
            clockcopter_prop.SetActive(false);
        }

        if(boss.activeInHierarchy && !alreadyDead)
        {
            if(!fadedOut)
            {
                MusicFadeOut();
            }
            

            bossUI.CrossFadeAlpha(1, 1, false);
            boss1UI.CrossFadeAlpha(1, 1, false);
            bossHealthUI.CrossFadeAlpha(1, 1, false);
            bossHealthBackUI.CrossFadeAlpha(1, 1, false);

            if(bossCurHealth <= 250)
            {
                bossSpawn.SetActive(true);
            }

            if(playerDead)
            {
                bossMusic.Stop();

                died = true;
            }
        }
        else
        {
            bossUI.CrossFadeAlpha(0, 1, false);
            boss1UI.CrossFadeAlpha(0, 1, false);
            bossHealthUI.CrossFadeAlpha(0, 1, false);
            bossHealthBackUI.CrossFadeAlpha(0, 1, false);
        }

        if(died)
        {
            bossUI.CrossFadeAlpha(0, 0.5f, false);
            boss1UI.CrossFadeAlpha(0, 0.5f, false);
            bossHealthUI.CrossFadeAlpha(0, 0.5f, false);
            bossHealthBackUI.CrossFadeAlpha(0, 0.5f, false);
            alreadyDead = true;
            died = false;
        }

        if(boss.activeInHierarchy && respawning)
        {
            bossMusic.Play();

            bossUI.CrossFadeAlpha(1, 0.5f, false);
            boss1UI.CrossFadeAlpha(1, 0.5f, false);
            bossHealthUI.CrossFadeAlpha(1, 0.5f, false);
            bossHealthBackUI.CrossFadeAlpha(1, 0.5f, false);
            alreadyDead = false;
            gateOpener.SetActive(true);
            gateCloser.SetActive(true);
        }

        if (gun2 && !gunPicked)
        {
            textBack.CrossFadeAlpha(1, 0.3f, false);
            weaponUse.CrossFadeAlpha(1, 0.3f, false);
            button.CrossFadeAlpha(1, 0.3f, false);

            timerToFade -= Time.deltaTime;
            if(timerToFade <= 0)
            {
                textBack.CrossFadeAlpha(0, 0.3f, false);
                weaponUse.CrossFadeAlpha(0, 0.3f, false);
                button.CrossFadeAlpha(0, 0.3f, false);
                gunPicked = true;
            }
        }

        if(playerDead && !boss.activeInHierarchy)
        {
            music.Stop();
        }

        if(respawning && !boss.activeInHierarchy)
        {
            music.Play();
        }
    }

    void MusicFadeIn()
    {
        bossMusic.volume -= Time.deltaTime / fadeTime;
        if (bossMusic.volume <= 0 && musicFaded)
        {
            music.volume += Time.deltaTime / fadeTime;
            if(music.volume >= 0.3f)
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
