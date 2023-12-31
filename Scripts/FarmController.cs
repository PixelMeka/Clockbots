using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource bossMusic;
    bool musicFaded = false;
    float fadeTime = 5f;
    bool fadedIn = false;
    bool fadedOut = false;
    bool musicPlay = false;

    public Image black;

    public GameObject cameraGate;
    public GameObject playerCamera;
    bool cameraShowing = false;
    float timerCamera = 4f;

    public GameObject needle1;
    public GameObject needle2;
    public GameObject needle3;
    public GameObject needle4;
    public GameObject needle5;
    public GameObject needle6;
    public GameObject needle7;
    public GameObject needle8;
    public GameObject needle9;
    public GameObject needle10;
    public GameObject needle11;
    public GameObject needle12;
    public GameObject needle13;
    public GameObject needle14;

    bool needle1Dead = false;
    bool needle2Dead = false;
    bool needle3Dead = false;
    bool needle4Dead = false;
    bool needle5Dead = false;
    bool needle6Dead = false;
    bool needle7Dead = false;
    bool needle8Dead = false;
    bool needle9Dead = false;
    bool needle10Dead = false;
    bool needle11Dead = false;
    bool needle12Dead = false;
    bool needle13Dead = false;
    bool needle14Dead = false;

    public GameObject root1;
    public GameObject root2;
    public GameObject root3;
    public GameObject root4;
    public GameObject root5;
    public GameObject root6;
    public GameObject root7;
    public GameObject root8;

    bool root1Dead;
    bool root2Dead;
    bool root3Dead;
    bool root4Dead;
    bool root5Dead;
    bool root6Dead;
    bool root7Dead;
    bool root8Dead;

    bool root1alreadyDead = false;
    bool root2alreadyDead = false;
    bool root3alreadyDead = false;
    bool root4alreadyDead = false;
    bool root5alreadyDead = false;
    bool root6alreadyDead = false;
    bool root7alreadyDead = false;
    bool root8alreadyDead = false;

    public GameObject rootTerrain1;
    public GameObject rootTerrain2;
    public GameObject rootTerrain3;
    public GameObject rootTerrain4;

    bool gateOpened = false;
    public Image needleUI;
    public Text needleText;

    public GameObject player;
    public GameObject respawnPad;
    public GameObject boss;
    public GameObject bossCollider;
    public GameObject bossHitBox;
    public Text pressE;
    public Image pressEBackground1;
    public Image pressEBackground2;
    public Image bossUI;
    public Image boss1UI;
    public Image boss2UI;
    public Image boss3UI;
    public Image bossHealthUI;
    public Image bossHealthBackUI;
    public GameObject bossCheckpoint;
    bool playerDead;
    bool collided;
    bool bossDead;
    bool isActive;
    bool respawning;
    bool alreadyDead;
    bool died = false;

    //Health values
    float totalCurHealth;
    float totalMaxHealth;
    float bossCurHealth;
    float bossMaxHealth;
    float root1CurHealth;
    float root1MaxHealth;
    float root2CurHealth;
    float root2MaxHealth;
    float root3CurHealth;
    float root3MaxHealth;
    float root4CurHealth;
    float root4MaxHealth;
    float root5CurHealth;
    float root5MaxHealth;
    float root6CurHealth;
    float root6MaxHealth;
    float root7CurHealth;
    float root7MaxHealth;
    float root8CurHealth;
    float root8MaxHealth;
    float root9CurHealth;
    float root9MaxHealth;
    float root10CurHealth;
    float root10MaxHealth;
    float root11CurHealth;
    float root11MaxHealth;
    float root12CurHealth;
    float root12MaxHealth;

    public GameObject gateBoss;
    public GameObject gateEnd;

    Animator animgateBoss;
    Animator animgateEnd;

    int needleCounter = 14;
    int rootCounter = 8;

    // Start is called before the first frame update
    void Start()
    {
        black.CrossFadeAlpha(0, 1, false);

        needleUI.canvasRenderer.SetAlpha(1f);
        needleText.canvasRenderer.SetAlpha(1f);

        bossUI.canvasRenderer.SetAlpha(0.0f);
        boss1UI.canvasRenderer.SetAlpha(0.0f);
        boss2UI.canvasRenderer.SetAlpha(0.0f);
        boss3UI.canvasRenderer.SetAlpha(0.0f);
        bossHealthUI.canvasRenderer.SetAlpha(0.0f);
        bossHealthBackUI.canvasRenderer.SetAlpha(0.0f);
        pressE.canvasRenderer.SetAlpha(0.0f);
        pressEBackground1.canvasRenderer.SetAlpha(0.0f);
        pressEBackground2.canvasRenderer.SetAlpha(0.0f);

        animgateBoss = gateBoss.GetComponentInChildren<Animator>();
        animgateEnd = gateEnd.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDead = player.GetComponent<PlayerStats>().isDead;
        respawning = respawnPad.GetComponentInChildren<RespawnDeath>().respawning;
        collided = bossCheckpoint.GetComponent<ControllerCollider>().collided;
        isActive = boss.GetComponent<AIRootBoss>().isActive;
        bossDead = bossHitBox.GetComponent<EnemyStats>().isDead;

        Needles();
        Roots();
        BossHealth();

        if(needleCounter <= 0 && !gateOpened)
        {
            needleUI.CrossFadeAlpha(0, 0.5f, false);
            needleText.CrossFadeAlpha(0, 0.5f, false);

            player.GetComponent<Move>().enabled = false;
            player.GetComponent<PlayerStats>().enabled = false;
            playerCamera.SetActive(false);
            cameraGate.SetActive(true);
            animgateBoss.SetBool("Open", true);
            gateOpened = true;
        }

        if(gateOpened && !cameraShowing)
        {
            timerCamera -= Time.deltaTime;
            if(timerCamera <= 0)
            {
                player.GetComponent<Move>().enabled = true;
                player.GetComponent<PlayerStats>().enabled = true;
                playerCamera.SetActive(true);
                cameraGate.SetActive(false);
                cameraShowing = true;
            }
        }

        //The player will be able to attack the boss
        if(rootCounter <=0 && !bossDead)
        {
            bossCollider.SetActive(false);
            bossHitBox.SetActive(true);
        }

        if(collided)
        {
            animgateBoss.SetBool("Open", false);
            bossCheckpoint.GetComponent<ControllerCollider>().collided = false;
        }

        if (bossDead)
        {
            if (!fadedIn)
            {
                if (!musicPlay)
                {
                    music.Play();
                    musicPlay = true;
                }
                MusicFadeIn();
            }

            animgateEnd.SetBool("Open", true);
            animgateBoss.SetBool("Open", true);

            bossCollider.SetActive(true);
            bossHitBox.SetActive(false);
        }

        if (isActive && !alreadyDead)
        {
            if (!fadedOut)
            {
                MusicFadeOut();
            }

            bossUI.CrossFadeAlpha(1, 1, false);
            boss3UI.CrossFadeAlpha(1, 1, false);
            bossHealthUI.CrossFadeAlpha(1, 1, false);
            bossHealthBackUI.CrossFadeAlpha(1, 1, false);

            if (playerDead)
            {
                bossMusic.Stop();

                died = true;
            }
        }
        else
        {
            bossUI.CrossFadeAlpha(0, 1, false);
            boss3UI.CrossFadeAlpha(0, 1, false);
            bossHealthUI.CrossFadeAlpha(0, 1, false);
            bossHealthBackUI.CrossFadeAlpha(0, 1, false);
        }

        if (died)
        {
            bossUI.CrossFadeAlpha(0, 0.5f, false);
            boss3UI.CrossFadeAlpha(0, 0.5f, false);
            bossHealthUI.CrossFadeAlpha(0, 0.5f, false);
            bossHealthBackUI.CrossFadeAlpha(0, 0.5f, false);
            alreadyDead = true;
            died = false;
        }

        if (isActive && respawning)
        {
            bossMusic.Play();

            bossUI.CrossFadeAlpha(1, 0.5f, false);
            boss3UI.CrossFadeAlpha(1, 0.5f, false);
            bossHealthUI.CrossFadeAlpha(1, 0.5f, false);
            bossHealthBackUI.CrossFadeAlpha(1, 0.5f, false);
            alreadyDead = false;
            animgateBoss.SetBool("Open", true);
            bossCheckpoint.SetActive(true);
        }

        if(playerDead && !isActive)
        {
            music.Stop();

            needleUI.CrossFadeAlpha(0, 0.5f, false);
            needleText.CrossFadeAlpha(0, 0.5f, false);
        }
        if(needleCounter != 0 && respawning)
        {
            music.Play();

            needleUI.CrossFadeAlpha(1, 0.5f, false);
            needleText.CrossFadeAlpha(1, 0.5f, false);
        }

        if(needleCounter == 0 && respawning && !isActive)
        {
            music.Play();
        }
    }

    //Checks which needlebots are dead
    void Needles()
    {
        needleText.text = needleCounter.ToString("0");

        if (needle1.activeInHierarchy == false && !needle1Dead)
        {
            needleCounter -= 1;
            needle1Dead = true;
        }
        if (needle2.activeInHierarchy == false && !needle2Dead)
        {
            needleCounter -= 1;
            needle2Dead = true;
        }
        if (needle3.activeInHierarchy == false && !needle3Dead)
        {
            needleCounter -= 1;
            needle3Dead = true;
        }
        if (needle4.activeInHierarchy == false && !needle4Dead)
        {
            needleCounter -= 1;
            needle4Dead = true;
        }
        if (needle5.activeInHierarchy == false && !needle5Dead)
        {
            needleCounter -= 1;
            needle5Dead = true;
        }
        if (needle6.activeInHierarchy == false && !needle6Dead)
        {
            needleCounter -= 1;
            needle6Dead = true;
        }
        if (needle7.activeInHierarchy == false && !needle7Dead)
        {
            needleCounter -= 1;
            needle7Dead = true;
        }
        if (needle8.activeInHierarchy == false && !needle8Dead)
        {
            needleCounter -= 1;
            needle8Dead = true;
        }
        if (needle9.activeInHierarchy == false && !needle9Dead)
        {
            needleCounter -= 1;
            needle9Dead = true;
        }
        if (needle10.activeInHierarchy == false && !needle10Dead)
        {
            needleCounter -= 1;
            needle10Dead = true;
        }
        if (needle11.activeInHierarchy == false && !needle11Dead)
        {
            needleCounter -= 1;
            needle11Dead = true;
        }
        if (needle12.activeInHierarchy == false && !needle12Dead)
        {
            needleCounter -= 1;
            needle12Dead = true;
        }
        if (needle13.activeInHierarchy == false && !needle13Dead)
        {
            needleCounter -= 1;
            needle13Dead = true;
        }
        if (needle14.activeInHierarchy == false && !needle14Dead)
        {
            needleCounter -= 1;
            needle14Dead = true;
        }
    }

    //Checks which roots are dead
    void Roots()
    {
        root1Dead = root1.GetComponent<EnemyStats>().isDead;
        root2Dead = root2.GetComponent<EnemyStats>().isDead;
        root3Dead = root3.GetComponent<EnemyStats>().isDead;
        root4Dead = root4.GetComponent<EnemyStats>().isDead;
        root5Dead = root5.GetComponent<EnemyStats>().isDead;
        root6Dead = root6.GetComponent<EnemyStats>().isDead;
        root7Dead = root7.GetComponent<EnemyStats>().isDead;
        root8Dead = root8.GetComponent<EnemyStats>().isDead;

        if (root1Dead && !root1alreadyDead)
        {
            rootCounter -= 1;
            root1alreadyDead = true;
        }
        if (root2Dead && !root2alreadyDead)
        {
            rootCounter -= 1;
            root2alreadyDead = true;
        }
        if (root3Dead && !root3alreadyDead)
        {
            rootCounter -= 1;
            root3alreadyDead = true;
        }
        if (root4Dead && !root4alreadyDead)
        {
            rootCounter -= 1;
            root4alreadyDead = true;
        }
        if (root5Dead && !root5alreadyDead)
        {
            rootCounter -= 1;
            root5alreadyDead = true;
        }
        if (root6Dead && !root6alreadyDead)
        {
            rootCounter -= 1;
            root6alreadyDead = true;
        }
        if (root7Dead && !root7alreadyDead)
        {
            rootCounter -= 1;
            root7alreadyDead = true;
        }
        if (root8Dead && !root8alreadyDead)
        {
            rootCounter -= 1;
            root8alreadyDead = true;
        }
    }

    //Total health of the boss
    void BossHealth()
    {
        bossCurHealth = bossHitBox.GetComponent<EnemyStats>().curHealth;
        bossMaxHealth = bossHitBox.GetComponent<EnemyStats>().maxHealth;
        root1CurHealth = root1.GetComponent<EnemyStats>().curHealth;
        root1MaxHealth = root1.GetComponent<EnemyStats>().maxHealth;
        root2CurHealth = root2.GetComponent<EnemyStats>().curHealth;
        root2MaxHealth = root2.GetComponent<EnemyStats>().maxHealth;
        root3CurHealth = root3.GetComponent<EnemyStats>().curHealth;
        root3MaxHealth = root3.GetComponent<EnemyStats>().maxHealth;
        root4CurHealth = root4.GetComponent<EnemyStats>().curHealth;
        root4MaxHealth = root4.GetComponent<EnemyStats>().maxHealth;
        root5CurHealth = root5.GetComponent<EnemyStats>().curHealth;
        root5MaxHealth = root5.GetComponent<EnemyStats>().maxHealth;
        root6CurHealth = root6.GetComponent<EnemyStats>().curHealth;
        root6MaxHealth = root6.GetComponent<EnemyStats>().maxHealth;
        root7CurHealth = root7.GetComponent<EnemyStats>().curHealth;
        root7MaxHealth = root7.GetComponent<EnemyStats>().maxHealth;
        root8CurHealth = root8.GetComponent<EnemyStats>().curHealth;
        root8MaxHealth = root8.GetComponent<EnemyStats>().maxHealth;
        root9CurHealth = rootTerrain1.GetComponent<EnemyStats>().curHealth;
        root9MaxHealth = rootTerrain1.GetComponent<EnemyStats>().maxHealth;
        root10CurHealth = rootTerrain2.GetComponent<EnemyStats>().curHealth;
        root10MaxHealth = rootTerrain2.GetComponent<EnemyStats>().maxHealth;
        root11CurHealth = rootTerrain3.GetComponent<EnemyStats>().curHealth;
        root11MaxHealth = rootTerrain3.GetComponent<EnemyStats>().maxHealth;
        root12CurHealth = rootTerrain4.GetComponent<EnemyStats>().curHealth;
        root12MaxHealth = rootTerrain4.GetComponent<EnemyStats>().maxHealth;

        totalCurHealth = root1CurHealth + root2CurHealth + root3CurHealth + root4CurHealth + root5CurHealth + root6CurHealth + root7CurHealth + root8CurHealth + root9CurHealth + root10CurHealth + root11CurHealth + root12CurHealth + bossCurHealth;
        totalMaxHealth = root1MaxHealth + root2MaxHealth + root3MaxHealth + root4MaxHealth + root5MaxHealth + root6MaxHealth + root7MaxHealth + root8MaxHealth + root9MaxHealth + root10MaxHealth + root11MaxHealth + root12MaxHealth + bossMaxHealth;
        bossHealthUI.rectTransform.sizeDelta = new Vector2((float)totalCurHealth / totalMaxHealth * 100, 123.7f);
    }

    void MusicFadeIn()
    {
        bossMusic.volume -= Time.deltaTime / fadeTime;
        if (bossMusic.volume <= 0 && musicFaded)
        {
            music.volume += Time.deltaTime / fadeTime;
            if (music.volume >= 0.3f)
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
