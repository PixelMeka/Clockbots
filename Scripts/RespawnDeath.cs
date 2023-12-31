using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnDeath : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform spawnPoint;

    public Image white;

    bool invokeOnce = false;
    public bool respawning = false;

    void Awake()
    {
        white.canvasRenderer.SetAlpha(0.0f);
    }
    //For respawning and turning fog on, fade in/out
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            respawning = true;
            white.CrossFadeAlpha(1, 1, false);
            if (!invokeOnce)
            {
                Invoke("FadeOut", 2.0f);
                invokeOnce = true;
            }
        }
    }
    void FadeOut()
    {
        white.CrossFadeAlpha(0, 1, false);
        player.transform.position = spawnPoint.transform.position;
        RenderSettings.fog = true;
        invokeOnce = false;
        respawning = false;
    }
}
