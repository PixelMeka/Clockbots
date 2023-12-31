using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallFadeOutIn : MonoBehaviour
{
    public Image black;
    bool invokeOnce = false;

    void Awake()
    {

    }
    //Only fades in/out to/from black, used for falls
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            black.CrossFadeAlpha(1, 0.5f, false);
            if (!invokeOnce)
            {
                Invoke("Fade", 1.0f);
                invokeOnce = true;
            }
        }
    }
    void Fade()
    {
        black.CrossFadeAlpha(0, 0.5f, false);
        invokeOnce = false;
    }
}
