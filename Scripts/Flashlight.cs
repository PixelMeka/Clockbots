using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The player's flashlight will be enabled and disabled when the 'F' key is pressed.
public class Flashlight : MonoBehaviour
{
    public AudioSource click;
    bool flash = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !flash)
        {
            click.Play();
            gameObject.GetComponentInChildren<Light>().enabled = true;
            flash = true;
        }

        else if (Input.GetKeyDown(KeyCode.F) && flash)
        {
            click.Play();
            gameObject.GetComponentInChildren<Light>().enabled = false;
            flash = false;
        }
    }
}
