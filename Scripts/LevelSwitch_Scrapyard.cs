using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//For triggering level changes
public class LevelSwitch_Scrapyard : MonoBehaviour
{
    public AudioSource music;

    public Image black;
    float timerToLoad = 0.5f;
    bool load;
    float fadeTime = 2f;

    void Update()
    {
        if (load)
        {
            music.volume -= Time.deltaTime / fadeTime;
            timerToLoad -= Time.deltaTime;
            if (timerToLoad <= 0)
            {
                PlayerPrefs.SetInt("curLevel", 3);
                SceneManager.LoadScene(5);
                load = false;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            black.CrossFadeAlpha(1, 0.5f, false);
            load = true;
        }
    }
}