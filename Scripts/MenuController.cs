using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script controls the main menu
public class MenuController : MonoBehaviour
{
    public AudioSource music;
    bool musicFaded = false;
    float fadeTime = 2f;

    public GameObject MenuUI;
    public GameObject stage;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    int curLevel;

    Animator anim;
    bool animstart = false;

    // Start is called before the first frame update
    void Start()
    {
        curLevel = PlayerPrefs.GetInt("curLevel");
        anim = stage.GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!animstart)
        {
            anim.SetBool("Up", true);
            animstart = true;
        }
    }

    //Starts a new game
    public void NewGame()
    {
        MusicFadeOut();
        PlayerPrefs.SetInt("curLevel", 2); //Resets the current level value to start up a new game
        SceneManager.LoadScene(5);
    }

    //Loads up the last visited level
    public void Continue()
    {
        MusicFadeOut();
        if(PlayerPrefs.HasKey("curLevel"))
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            PlayerPrefs.SetInt("curLevel", 2);
            SceneManager.LoadScene(5);
        }
    }

    //Turn on the options menu
    public void Options()
    {
        MenuUI.SetActive(false);
        optionsMenu.SetActive(true);
    }

    //View the credits
    public void Credits()
    {
        MenuUI.SetActive(false);
        creditsMenu.SetActive(true);
    }

    //To quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        MusicFadeOut();
        PlayerPrefs.SetInt("curLevel", 1);
        SceneManager.LoadScene(5);
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
