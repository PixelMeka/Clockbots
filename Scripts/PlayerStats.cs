using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public AudioSource superFullAudio;
    public AudioSource superUsedAudio;
    public AudioSource deathAudio;
    public AudioSource superPickAudio;
    public AudioSource healthPickAudio;
    public AudioSource hitSound;
    public AudioSource bigHitSound;

    bool superFull = false;
    bool issuperUsedAudio = false;
    bool dAudio = false;

    public float CurHealth;
    public float MaxHealth;
    public float CurStamina;
    public float MaxStamina;
    public float CurSuperPower;
    public float MaxSuperPower;
    public bool isDead = false;
    public bool isDying = false;
    public bool superUsed = false;
    public bool bulletHit;
    public bool pelletHit;
    public bool fireHit;
    bool died = false;

    //In order for the StartCoroutine to work properly inside the Update function and to prevent it from running each frame, a bool trigger has to be set up
    //In this case, StartCoroutine will run only when startflicker is set to true
    //To stop the flickering, startflicker has to be set to false after each if statement
    bool startflicker = false;
    Color greenblue = new Color(0, 1, 0.04f);   //Sets a new color, slightly bluish green in this case
    Light bulb;     
    GameObject bulbcore;

    Transform stamina;
    Vector3 staminascale;
    Transform super;
    Vector3 superscale;

    bool isRunning;
    bool isCrouching;
    bool isAir;
    public bool isStaminaEnd;
    bool staminaRegen = false;
    bool delay = false;
    bool invokeOnce = false;
    bool invokeOnce2 = false;
    public bool damaged = false;

    //For HUD and UI
    public Image healthUI;
    public Image staminaUI;
    public Image superUI;
    public Image damageUI;
    public Image hazardDamageUI;
    bool hazard = false;
    public Image superUsedUI;
    public Image superStartUI;
    public Image superStartUI2;
    public Image superStartUI3;
    public Image superStartUIWhite;
    public Image superStartUIBlack;
    bool superWhite = false;
    float timerSuperWhite = 1f;
    public Text press_Q;

    //For dying
    [SerializeField]
    private Transform deathPoint;
    public Image black;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        damageUI.canvasRenderer.SetAlpha(0.0f);
        hazardDamageUI.canvasRenderer.SetAlpha(0.0f);
        superUsedUI.canvasRenderer.SetAlpha(0.0f);
        superStartUI.canvasRenderer.SetAlpha(0.0f);
        superStartUI2.canvasRenderer.SetAlpha(0.0f);
        superStartUI3.canvasRenderer.SetAlpha(0.0f);
        superStartUIWhite.canvasRenderer.SetAlpha(0.0f);
        superStartUIBlack.canvasRenderer.SetAlpha(0.0f);
        press_Q.canvasRenderer.SetAlpha(0.0f);

        MaxHealth = 100;
        CurHealth = MaxHealth;

        MaxStamina = 100;
        CurStamina = MaxStamina;

        MaxSuperPower = 100;
        CurSuperPower = 0;

        anim = GetComponentInChildren<Animator> ();

        //To find the light bulb object's light component
        bulb = GameObject.Find("BulbLight").GetComponent<Light>();
        //To find the object inside the bulb
        bulbcore = GameObject.Find("BulbCore");

        //Stamina and Super Power bar transforms and scaling
        stamina = GameObject.Find("Stamina").GetComponent<Transform>();
        staminascale = stamina.localScale;
        super = GameObject.Find("Super").GetComponent<Transform>();
        superscale = super.localScale;
    }
    //This function manages the pickup system, and checks what hit the player.
    private void OnTriggerEnter(Collider other)
    {
        //Pickups:
        if (other.tag == "Health")
        {
            if (CurHealth < MaxHealth)
            {
                healthPickAudio.Play();
                CurHealth += 20;
                Destroy(other.gameObject);
            }
        }
        if (other.tag == "Super Health")
        {
            if (CurHealth < MaxHealth)
            {
                healthPickAudio.Play();
                CurHealth += 100;
                Destroy(other.gameObject);
            }
        }
        if (other.tag == "Stamina")
        {
            if(CurStamina < MaxStamina)
            {
                healthPickAudio.Play();
                CurStamina += 20;
                Destroy(other.gameObject);
            }
        }
        if (other.tag == "Super")
        {
            if (CurSuperPower < MaxSuperPower)
            {
                superPickAudio.Play();
                CurSuperPower += 50;
                Destroy(other.gameObject);
            }
        }
        
        //Enemy attacks:
        if(other.tag == "Punch" && !damaged)
        {
            hitSound.Play();
            CurHealth -= 10;
            damaged = true;
            StartCoroutine("DamageDelay");
        }

        if(other.tag == "Bullet")
        {
            hitSound.Play();
            CurHealth -= 3;
            damaged = true;
            StartCoroutine("DamageDelay");
        }

        if(other.tag == "Explosion" && !damaged)
        {
            bigHitSound.Play();
            CurHealth -= 40;
            damaged = true;
            StartCoroutine("DamageDelay");
        }

        if(other.tag == "Player Pellet")
        {
            hitSound.Play();
            CurHealth -= 20;
            damaged = true;
            StartCoroutine("DamageDelay");
        }

        if (other.tag == "Player Explosion")
        {
            hitSound.Play();
            CurHealth -= 20;
            damaged = true;
            StartCoroutine("DamageDelay");
        }
    }
    //If player is inside a collider
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "HealthBeam")
        {
            if(CurHealth < MaxHealth)
            {
                CurHealth += 2f * Time.deltaTime;
            }
        }

        if(other.tag == "Hazard")
        {
            CurHealth -= 10f * Time.deltaTime;
            if(!hazard)
            {
                hazard = true;
                hazardDamageUI.CrossFadeAlpha(1, 0.5f, false);
            }
        }
        else
        {
            hazardDamageUI.CrossFadeAlpha(0, 1, false);
            hazard = false;
        }
    }

    //To stop the player from taking too much damage from a single attack.
    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(0.7f);
        damaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = gameObject.GetComponent<Move>().isRunning;
        isCrouching = gameObject.GetComponent<Move>().isCrouching;
        isAir = gameObject.GetComponent<Move>().isAir;

        Health();
        LightBulb();
        Stamina();
        StaminaRun();
        Super();
        Death();

        //To check if stamina is over
        if(CurStamina == 0 || CurStamina < 0)
        {
            isStaminaEnd = true;
        }
        if(CurStamina > 0)
        {
            isStaminaEnd = false;
        }
    }

    //To die and fade in/out screen to black. Disables movement for a brief amount of time. Invoke creates delays.
    //The entire process is written in more than one function.
    void Death()
    {
        if (isDead == true)
        {
            if(!dAudio)
            {
                deathAudio.Play();
                dAudio = true;
            }
            
            isDying = true;
            gameObject.GetComponent<Move>().enabled = false;
            anim.SetBool("Dead", true);
            if(!invokeOnce)
            {
                Invoke("DeathTwo", 4.0f);
                invokeOnce = true;
            }
        }
    }
    void DeathTwo()
    {
        black.CrossFadeAlpha(1, 1, false);
        if (!invokeOnce2)
        {
            Invoke("DeathThree", 2.0f);
            invokeOnce2 = true;
        }
    }
    void DeathThree()
    {
        anim.SetBool("Dead", false);
        black.CrossFadeAlpha(0, 1, false);
        this.transform.position = deathPoint.transform.position;
        CurHealth = MaxHealth;
        CurStamina = MaxStamina;
        CurSuperPower = 0;
        invokeOnce = false;
        invokeOnce2 = false;
        gameObject.GetComponent<Move>().enabled = true;
        isDying = false;
        dAudio = false;
    }

    //For health
    void Health()
    {
        healthUI.rectTransform.sizeDelta = new Vector2((float) CurHealth/MaxHealth * 100, 123.7f);

        if(CurHealth > MaxHealth)
        {
            CurHealth = MaxHealth;
        }
        if(CurHealth <= 0)
        {
            isDead = true;
        }
        if(CurHealth > 0)
        {
            isDead = false;

            if(died)
            {
                damageUI.CrossFadeAlpha(0, 0.5f, false);
                died = false;
            }
        }
    }
    //This function changes the color of the light bulb (left hand) according to the player's current health value. Also changes the opacity of the damage UI.
    void LightBulb()
    {
        if (CurHealth > 75)
        {
            startflicker = false;
            bulb.color = greenblue;
            bulbcore.SetActive(true);

            damageUI.canvasRenderer.SetAlpha(0.0f);
        }

        if (CurHealth > 50 && CurHealth < 76)
        {
            startflicker = false;
            bulb.color = Color.green;

            damageUI.CrossFadeAlpha(0.25f, 0.5f, false);
        }

        if (CurHealth > 25 && CurHealth < 51)
        {
            startflicker = false;
            bulb.color = Color.yellow;

            damageUI.CrossFadeAlpha(0.5f, 0.5f, false);
        }

        if (CurHealth > 10 && CurHealth < 26)
        {
            startflicker = false;
            bulb.color = Color.red;

            damageUI.CrossFadeAlpha(0.75f, 0.5f, false);
        }

        if (CurHealth > 0 && CurHealth < 11)
        {
            //To make the light flicker uniformly
            if(!startflicker)
            {
                StartCoroutine("Flicker");
            }

            damageUI.CrossFadeAlpha(0.75f, 0.5f, false);
        }
        //If the player is dead, the red light will slowly fade
        if (isDead == true)
        {
            startflicker = false;
            bulbcore.SetActive(false);
            bulb.color -= (Color.red / 1.0f) * Time.deltaTime;

            damageUI.CrossFadeAlpha(0.75f, 0.5f, false);
            died = true;
        }
    }
    //This function enables the flickering of the light when the health is low
    //It waits for a set amount of time before turning the light on or off, this is controlled by StartCoroutine
    IEnumerator Flicker()
    {
        startflicker = true;
        while(startflicker)
        {
            bulbcore.SetActive(false);
            bulb.color = Color.black;
            yield return new WaitForSeconds(0.5f);
            bulbcore.SetActive(true);
            bulb.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            bulbcore.SetActive(false);
            bulb.color = Color.black;
            yield return new WaitForSeconds(0.5f);
            bulbcore.SetActive(true);
            bulb.color = Color.red;
            yield return new WaitForSeconds(1.5f);
        }
    }
    //This function changes the size (y-axis) of the stamina bar according to the current stamina value
    void Stamina()
    {
        //For the hand
        staminascale.y = CurStamina/MaxStamina;
        stamina.transform.localScale = staminascale;
        //For UI
        staminaUI.rectTransform.sizeDelta = new Vector2(staminascale.y * 65, 5.7f);
    }
    //If the player is pressing shift and moving, stamina will decrease over time
    void StaminaRun()
    {
        float rightleft = Input.GetAxis("Horizontal");  
        float forback = Input.GetAxis("Vertical");

        if (isRunning && !isCrouching)
        {
            if (forback != 0 || rightleft != 0)
            {
                if(!isAir)
                {
                    staminaRegen = false;
                    CurStamina -= 20f * Time.deltaTime;
                }
            }
        }
        //If the player is not running, stamina will regenerate over time, after a short delay
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            delay = true;
        }
        if (delay)
        {
            StartCoroutine("StaminaDelay");
        }
        if (staminaRegen && CurStamina <= MaxStamina)
        {
            CurStamina += 40f * Time.deltaTime;
        }
        if(CurStamina > MaxStamina)
        {
            CurStamina = MaxStamina;
        }
    }
    //This function will create a delay before the stamina starts filling up
    IEnumerator StaminaDelay()
    {
        staminaRegen = false;
        delay = false;
        yield return new WaitForSeconds(2f);
        staminaRegen = true;
    }
    //This function is all about the super and changes the size (y-axis) of the super power bar according to the current power value
    void Super()
    {
        superscale.y = CurSuperPower / MaxSuperPower;
        super.transform.localScale = superscale;

        superUI.rectTransform.sizeDelta = new Vector2(superscale.y * 65, 5.7f);

        if (CurSuperPower <= 0)
        {
            issuperUsedAudio = false;
            superFull = false;
            superUsed = false;
            superUsedUI.CrossFadeAlpha(0, 0.5f, false);

            superStartUI.CrossFadeAlpha(0, 0.2f, false);
            superStartUI2.CrossFadeAlpha(0f, 0.2f, false);
            superStartUI3.CrossFadeAlpha(0f, 0.2f, false);
            superStartUIWhite.CrossFadeAlpha(0, 0.2f, false);
            superStartUIBlack.CrossFadeAlpha(0, 0.2f, false);
            press_Q.CrossFadeAlpha(0, 0.2f, false);
        }
        if (CurSuperPower > MaxSuperPower)
        {
            CurSuperPower = MaxSuperPower;
        }
        //If the player's bullet hits an enemy, slightly increase the super power value.
        if(bulletHit)
        {
            CurSuperPower += 3;
            bulletHit = false;
        }
        if (pelletHit)
        {
            CurSuperPower += 5;
            pelletHit = false;
        }
        if(fireHit)
        {
            CurSuperPower += 0.1f;
            fireHit = false;
        }
        //When the super power meter is full, the player will be prompted to press Q in order to use it.
        if (CurSuperPower >= MaxSuperPower)
        {
            if(!superFull)
            {
                superFullAudio.Play();
                superFull = true;
            }
            
            superStartUI.CrossFadeAlpha(1f, 0.2f, false);
            superStartUI2.CrossFadeAlpha(1f, 0.2f, false);
            superStartUI3.CrossFadeAlpha(1f, 0.2f, false);
            superStartUIBlack.CrossFadeAlpha(1, 0.2f, false);
            press_Q.CrossFadeAlpha(1, 0.2f, false);

            //The white outline of the super "Q" prompt will flash
            if (!superWhite)
            {
                timerSuperWhite -= Time.deltaTime;
                if (timerSuperWhite <= 0.0f)
                {
                    superStartUIWhite.CrossFadeAlpha(1, 0.2f, false);
                    superStartUIBlack.CrossFadeAlpha(0, 0.2f, false);
                    timerSuperWhite = 1f;
                    superWhite = true;
                }
            }
            if(superWhite)
            {
                timerSuperWhite -= Time.deltaTime;
                if (timerSuperWhite <= 0.0f)
                {
                    superStartUIWhite.CrossFadeAlpha(0, 0.2f, false);
                    superStartUIBlack.CrossFadeAlpha(1, 0.2f, false);
                    timerSuperWhite = 1f;
                    superWhite = false;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(!issuperUsedAudio)
                {
                    superUsedAudio.Play();
                    issuperUsedAudio = true;
                }
                superUsed = true;
                superUsedUI.CrossFadeAlpha(1, 0.5f, false);

                superStartUI.CrossFadeAlpha(0, 0.2f, false);
                superStartUI2.CrossFadeAlpha(0f, 0.2f, false);
                superStartUI3.CrossFadeAlpha(0f, 0.2f, false);
                superStartUIWhite.CrossFadeAlpha(0, 0.2f, false);
                superStartUIBlack.CrossFadeAlpha(0, 0.2f, false);
                press_Q.CrossFadeAlpha(0, 0.2f, false);
            }
        }
        //The super power meter will then decrease over time.
        if(superUsed && CurSuperPower != 0)
        {
            CurSuperPower -= 10f * Time.deltaTime;
        }
        
    }
}
