using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeCollider : MonoBehaviour
{
    public AudioSource turnSound;

    public bool turn = false;
    bool entered = false;
    public Image pressEBackground;
    public Image pressEBackground2;
    public Text pressEText;

    void Start()
    {
        pressEBackground.canvasRenderer.SetAlpha(0.0f);
        pressEBackground2.canvasRenderer.SetAlpha(0.0f);
        pressEText.canvasRenderer.SetAlpha(0.0f);
    }

    void Update()
    {
        if(turn)
        {
            turnSound.Play();

            pressEBackground.CrossFadeAlpha(0, 0.3f, false);
            pressEBackground2.CrossFadeAlpha(0, 0.3f, false);
            pressEText.CrossFadeAlpha(0, 0.3f, false);

            gameObject.SetActive(false);
        }

        if(entered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                turn = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pressEBackground.CrossFadeAlpha(1, 0.3f, false);
            pressEBackground2.CrossFadeAlpha(1, 0.3f, false);
            pressEText.CrossFadeAlpha(1, 0.3f, false);

            entered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pressEBackground.CrossFadeAlpha(0, 0.3f, false);
            pressEBackground2.CrossFadeAlpha(0, 0.3f, false);
            pressEText.CrossFadeAlpha(0, 0.3f, false);

            entered = false;
        }
    }

}
