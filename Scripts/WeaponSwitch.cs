using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public AudioSource gunPickAudio;

    public int curWeapon;
    public bool isSwapping = false;
    public int weaponType = 1; //1 for punch, 2 for clockgun, 3 for pelletter, 4 for flamethrower, 5 for pumpkin launcher
    bool isDying;
    Animator anim;
    bool reloading;
    public Image crosshair1;
    public Image crosshair2;
    public Image crosshair3;

    public Image gun0UI;
    public Image gun1UI;
    public Image gun2UI;
    public Image gun3UI;

    public GameObject punch;
    public GameObject clockgun;
    public GameObject pelletter;
    public GameObject flamethrower;

    //To hide the right arm sometimes
    public GameObject rArm;
    public GameObject details;

    //Unlock tags
    public bool Gun0 = true;
    public bool Gun1 = false;
    public bool Gun2 = false;
    public bool Gun3 = false;

    //This function manages the gun pickup and unlock system.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Gun1")
        {
            gunPickAudio.Play();
            Gun1 = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "Gun2")
        {
            gunPickAudio.Play();
            Gun2 = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "Gun3")
        {
            gunPickAudio.Play();
            Gun3 = true;
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        curWeapon = 1;

        crosshair1.canvasRenderer.SetAlpha(0.0f);
        crosshair2.canvasRenderer.SetAlpha(0.0f);
        crosshair3.canvasRenderer.SetAlpha(0.0f);

        gun0UI.canvasRenderer.SetAlpha(1f);
        gun1UI.canvasRenderer.SetAlpha(0f);
        gun2UI.canvasRenderer.SetAlpha(0f);
        gun3UI.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        isDying = GetComponent<PlayerStats>().isDying;
        reloading = GetComponent<PlayerCombat>().reloading;

        if(!isDying && !reloading)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Gun0)
            {
                if (curWeapon != 1)
                {
                    anim.SetTrigger("SwitchW");
                    anim.SetInteger("WeaponType", 1);
                    isSwapping = true;
                    weaponType = 1;

                    crosshair1.canvasRenderer.SetAlpha(0.0f);
                    crosshair2.canvasRenderer.SetAlpha(0.0f);
                    crosshair3.canvasRenderer.SetAlpha(0.0f);

                    gun0UI.canvasRenderer.SetAlpha(1f);
                    gun1UI.canvasRenderer.SetAlpha(0f);
                    gun2UI.canvasRenderer.SetAlpha(0f);
                    gun3UI.canvasRenderer.SetAlpha(0f);

                    Invoke("WeaponSwap", 0.7f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && Gun1)
            {
                if (curWeapon != 2)
                {
                    anim.SetTrigger("SwitchW");
                    anim.SetInteger("WeaponType", 2);
                    isSwapping = true;
                    weaponType = 2;

                    crosshair1.canvasRenderer.SetAlpha(1f);
                    crosshair2.canvasRenderer.SetAlpha(0.0f);
                    crosshair3.canvasRenderer.SetAlpha(0.0f);

                    gun0UI.canvasRenderer.SetAlpha(0f);
                    gun1UI.canvasRenderer.SetAlpha(1f);
                    gun2UI.canvasRenderer.SetAlpha(0f);
                    gun3UI.canvasRenderer.SetAlpha(0f);

                    Invoke("WeaponSwap", 0.7f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && Gun2)
            {
                if (curWeapon != 3)
                {
                    anim.SetTrigger("SwitchW");
                    anim.SetInteger("WeaponType", 3);
                    isSwapping = true;
                    weaponType = 3;

                    crosshair1.canvasRenderer.SetAlpha(0f);
                    crosshair2.canvasRenderer.SetAlpha(1f);
                    crosshair3.canvasRenderer.SetAlpha(0.0f);

                    gun0UI.canvasRenderer.SetAlpha(0f);
                    gun1UI.canvasRenderer.SetAlpha(0f);
                    gun2UI.canvasRenderer.SetAlpha(1f);
                    gun3UI.canvasRenderer.SetAlpha(0f);

                    Invoke("WeaponSwap", 0.7f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && Gun3)
            {
                if (curWeapon != 4)
                {
                    anim.SetTrigger("SwitchW");
                    anim.SetInteger("WeaponType", 4);
                    isSwapping = true;
                    weaponType = 4;

                    crosshair1.canvasRenderer.SetAlpha(0f);
                    crosshair2.canvasRenderer.SetAlpha(0f);
                    crosshair3.canvasRenderer.SetAlpha(1f);

                    gun0UI.canvasRenderer.SetAlpha(0f);
                    gun1UI.canvasRenderer.SetAlpha(0f);
                    gun2UI.canvasRenderer.SetAlpha(0f);
                    gun3UI.canvasRenderer.SetAlpha(1f);

                    Invoke("WeaponSwap", 0.7f);
                }
            }
        }
    }

    //Changes the current weapon based on the player's input. Also waits a bit before changing the weapon.
    void WeaponSwap()
    {
        if (weaponType == 1)
        {
            punch.SetActive(true);
            rArm.SetActive(true);
            details.SetActive(true);
            clockgun.SetActive(false);
            pelletter.SetActive(false);
            flamethrower.SetActive(false);
            curWeapon = 1;
            isSwapping = false;
        }

        if (weaponType == 2)
        {
            punch.SetActive(false);
            rArm.SetActive(false);
            details.SetActive(false);
            clockgun.SetActive(true);
            pelletter.SetActive(false);
            flamethrower.SetActive(false);
            curWeapon = 2;
            isSwapping = false;
        }

        if (weaponType == 3)
        {
            punch.SetActive(false);
            rArm.SetActive(false);
            details.SetActive(false);
            clockgun.SetActive(false);
            pelletter.SetActive(true);
            flamethrower.SetActive(false);
            curWeapon = 3;
            isSwapping = false;
        }

        if (weaponType == 4)
        {
            punch.SetActive(false);
            rArm.SetActive(false);
            details.SetActive(false);
            clockgun.SetActive(false);
            pelletter.SetActive(false);
            flamethrower.SetActive(true);
            curWeapon = 4;
            isSwapping = false;
        }
    }
}
