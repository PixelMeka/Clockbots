using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDestroyer : MonoBehaviour
{
    float timerToDie = 1f;
    // Update is called once per frame
    void Update()
    {
        timerToDie -= Time.deltaTime;
        if(timerToDie <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
