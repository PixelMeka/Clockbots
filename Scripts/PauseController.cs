using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script controls the pause menu
public class PauseController : MonoBehaviour
{
    private AudioSource[] allAudio;
    GameObject player;

    public static bool paused = false;
    public GameObject mainCam;
    public GameObject pauseMainUI;
    public GameObject pauseMenuUI;
    public GameObject optionsMenu;

    int curLevel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        curLevel = PlayerPrefs.GetInt("curLevel");

        pauseMainUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
            {
                player.GetComponent<PlayerCombat>().enabled = true;
                Resume();
                ResumeAudio();
            }
            else
            {
                player.GetComponent<PlayerCombat>().enabled = false;
                Pause();
                PauseAudio();
            }
        }
    }

    //To resume the game
    public void Resume()
    {
        player.GetComponent<PlayerCombat>().enabled = true;

        ResumeAudio();

        pauseMainUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;

        mainCam.GetComponent<CameraMove>().enabled = true;
        mainCam.GetComponent<CameraSway>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //To pause the game
    void Pause()
    {
        pauseMainUI.SetActive(true);
        pauseMenuUI.SetActive(true);
        optionsMenu.SetActive(false);
        Time.timeScale = 0f;
        paused = true;

        mainCam.GetComponent<CameraMove>().enabled = false;
        mainCam.GetComponent<CameraSway>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //View the options
    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
    }

    //Restart the current level
    public void Restart()
    {
        paused = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene(curLevel);
    }

    //Quit to the main menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void PauseAudio()
    {
        allAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource sound in allAudio)
        {
            sound.Pause();
        }
    }

    void ResumeAudio()
    {
        allAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource sound in allAudio)
        {
            sound.UnPause();
        }
    }
}
