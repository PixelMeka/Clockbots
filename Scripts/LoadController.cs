using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script controls the loading screens and the actual loading of levels.
public class LoadController : MonoBehaviour
{
    public AudioSource music;
    bool musicFaded = false;
    float fadeTime = 2f;

    AsyncOperation asyncOperation; //To load the levels in the background while this scene is playing

    //Different objects to spawn to simulate different screens
    public GameObject TownObj;
    public GameObject ScrapObj;
    public GameObject FarmObj;

    public Image black;
    public Text Loading;
    public Text Enter;
    public Text Tutorial;
    public Text Town;
    public Text Scrapyard;
    public Text Farm;
    bool loading = true;
    bool loadLevel = false;
    bool started = false;
    bool activated = false;

    float timerAnim = 0.5f;
    float timerExtra = 2f;
    float timerEnd = 1f;

    public GameObject stage;
    Animator anim;

    int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        timerAnim = 0.5f;
        timerExtra = 2f;
        timerEnd = 1f;

        //PlayerPrefs saves the values throughout the game session
        level = PlayerPrefs.GetInt("curLevel");

        anim = stage.GetComponentInChildren<Animator>();
        black.canvasRenderer.SetAlpha(1f);
        Loading.canvasRenderer.SetAlpha(0f);
        Enter.canvasRenderer.SetAlpha(0f);
        Tutorial.canvasRenderer.SetAlpha(0f);
        Town.canvasRenderer.SetAlpha(0f);
        Scrapyard.canvasRenderer.SetAlpha(0f);
        Farm.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        //First state of the scene
        if(loading && !started)
        {
            black.CrossFadeAlpha(0, 0.5f, false);

            //To spawn the objects
            if (level == 1)
            {
                TownObj.SetActive(false);
                ScrapObj.SetActive(false);
                FarmObj.SetActive(false);
            }
            if (level == 2)
            {
                TownObj.SetActive(true);
            }
            if (level == 3)
            {
                ScrapObj.SetActive(true);
            }
            if (level == 4)
            {
                FarmObj.SetActive(true);
            }

            //Play the stage animation after some time
            timerAnim -= Time.deltaTime;
            if(timerAnim <= 0)
            {
                Loading.CrossFadeAlpha(1, 0.3f, false);
                anim.SetTrigger("Up");
                anim.SetBool("Walk", true);
                started = true;
            }
        }

        //To check which level has to be loaded
        if(started && !activated)
        {
            if (level == 1)
            {
                Tutorial.CrossFadeAlpha(1, 0.3f, false);

                //Start the loading process:
                asyncOperation = SceneManager.LoadSceneAsync(1);
                asyncOperation.allowSceneActivation = false;
                activated = true;
            }
            if (level == 2)
            {
                Town.CrossFadeAlpha(1, 0.3f, false);

                //Start the loading process:
                asyncOperation = SceneManager.LoadSceneAsync(2);
                asyncOperation.allowSceneActivation = false;
                activated = true;
            }
            if (level == 3)
            {
                Scrapyard.CrossFadeAlpha(1, 0.3f, false);

                //Start the loading process:
                asyncOperation = SceneManager.LoadSceneAsync(3);
                asyncOperation.allowSceneActivation = false;
                activated = true;
            }
            if (level == 4)
            {
                Farm.CrossFadeAlpha(1, 0.3f, false);

                //Start the loading process:
                asyncOperation = SceneManager.LoadSceneAsync(4);
                asyncOperation.allowSceneActivation = false;
                activated = true;
            }
        }

        //When the loading operation ends
        if (asyncOperation.progress >= 0.9f)
        {
            timerExtra -= Time.deltaTime;
            if(timerExtra <= 0)
            {
                loading = false;
            }
        }

        //If the loading has ended, the player will be promted to press enter in order to continue
        if (!loading)
        {
            Loading.CrossFadeAlpha(0, 0.3f, false);
            Enter.CrossFadeAlpha(1, 0.3f, false);
            if(Input.GetKeyDown(KeyCode.Return))
            {
                black.CrossFadeAlpha(1, 0.5f, false);
                Loading.CrossFadeAlpha(0, 0.3f, false);
                Enter.CrossFadeAlpha(0, 0.3f, false);
                anim.SetBool("Walk", false);

                loadLevel = true;
            }
        }

        //Which level to actually load after the loading
        if(loadLevel)
        {
            MusicFadeOut();

            timerEnd -= Time.deltaTime;
            if (timerEnd <= 0)
            {
                if (level == 1)
                {
                    SceneManager.LoadScene(1);
                }
                if (level == 2)
                {
                    SceneManager.LoadScene(2);
                }
                if (level == 3)
                {
                    SceneManager.LoadScene(3);
                }
                if (level == 4)
                {
                    SceneManager.LoadScene(4);
                }
            }
        }
    }
    void MusicFadeOut()
    {
        music.volume -= Time.deltaTime / fadeTime;
        if (music.volume <= 0 && !musicFaded)
        {
            music.Stop();
            musicFaded = true;
        }
    }
}
