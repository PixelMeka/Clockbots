using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//For triggering level changes
public class LevelSwitch_Tutorial : MonoBehaviour
{
    public Image black;
    float timerToLoad = 0.5f;
    bool load;

    void Update()
    {
        if (load)
        {
            timerToLoad -= Time.deltaTime;
            if (timerToLoad <= 0)
            {
                PlayerPrefs.SetInt("curLevel", 1);
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
