using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//The last screen of the game
public class EndController : MonoBehaviour
{
    public Image black;
    public Text Thanks;
    public Text Enter;
    bool started = true;
    bool loadmenu = false;

    float timerAnim = 0.5f;
    float timerEnd = 1f;

    public GameObject stage;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        timerAnim = 0.5f;
        timerEnd = 1f;

        anim = stage.GetComponentInChildren<Animator>();
        black.canvasRenderer.SetAlpha(1f);
        Enter.canvasRenderer.SetAlpha(0f);
        Thanks.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            black.CrossFadeAlpha(0, 0.5f, false);

            //Play the stage animation after some time
            timerAnim -= Time.deltaTime;
            if(timerAnim <= 0)
            {
                Enter.CrossFadeAlpha(1, 0.3f, false);
                Thanks.CrossFadeAlpha(1, 0.3f, false);
                anim.SetBool("Up", true);
                started = false;
            }
        }

        if(!started)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                black.CrossFadeAlpha(1, 1.5f, false);
                Enter.CrossFadeAlpha(0, 0.3f, false);
                Thanks.CrossFadeAlpha(0, 0.3f, false);
                anim.SetBool("Up", false);

                loadmenu = true;
            }
        }

        if(loadmenu)
        {
            timerEnd -= Time.deltaTime;
            if (timerEnd <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
