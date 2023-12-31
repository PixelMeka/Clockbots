using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public AudioSource punchAudio;
    public AudioSource punchSPAudio;

    public AudioSource clockgunShot;
    public AudioSource clockgunShotSP;
    public AudioSource reloadC;

    public AudioSource pelShot;
    public AudioSource pelShotSP;
    public AudioSource reloadP;

    public AudioSource fireShot;
    public AudioSource fireShotSP;
    public AudioSource fireShotAlt;
    public AudioSource fireShotSPAlt;
    public AudioSource fireShotStart;

    bool isFireSound = false;
    bool relSound = false;
    bool punchWait = false;
    public Text ammoCounter;
    public Transform barrelC;
    public Transform barrelP;
    public GameObject bulletN;
    public GameObject bulletS;
    public GameObject bulletF;
    public GameObject bulletSF;
    public GameObject muzzleFlash1;
    public GameObject muzzleFlash2;
    public GameObject superMuzzleFlash1;
    public GameObject superMuzzleFlash2;
    public float force;
    bool shot1 = false;
    bool shot2 = false;
    Animator anim;
    bool isDying = false;
    bool superUsed = false;
    public bool reloading = false;
    int bullets; //To limit how many shots the player will be able to make in a single reload
    bool assigned = false;

    //Center of the screen
    float x;
    float y;

    //For Flamethrower
    public GameObject flameMeter;
    bool fireParticlePlaying = false;
    public GameObject fireParticle;
    public GameObject superfireParticle;
    public GameObject fireAltParticle;
    public GameObject superfireAltParticle;
    public GameObject fire;
    public GameObject superfire;
    public GameObject fireAlt;
    public GameObject superfireAlt;
    Animator animFlame;
    bool fireStart = false;
    bool firecollider = false;
    float timerFlamePri = 0.05f;
    float timerFlameAlt = 1.3f;
    float timerFlameAltCollider = 0.15f;
    float timerFlameReload = 1.5f;

    //For Pelletter
    public GameObject pelletter;
    Animator animPel;
    float timerPelPri = 0.3f;
    float timerPelAlt = 1f;
    float timerPelReload = 1.5f;
    public GameObject muzzleFlashPel1;
    public GameObject muzzleFlashPel2;
    public GameObject superMuzzleFlashPel1;
    public GameObject superMuzzleFlashPel2;
    public GameObject smokeReloadPel;
    bool smoke = false;
    public GameObject pellet;
    public GameObject pelletS;
    public GameObject pelletSticky;
    public GameObject pelletSuperSticky;

    //For Clockgun
    public GameObject gear;
    float angle = 140;
    float timerClockgunPri = 0.5f;
    float timerClockgunAlt = 0.1f;
    float timerClockgunReload = 1f;

    //For punching
    public GameObject superLight;
    public GameObject attackCollider;
    public GameObject attackSCollider;
    float timerPunch = 0.5f;

    //From weapon switching
    int curWeapon;
    bool isSwapping;

    //For continuous fire
    float timerShooting = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        animPel = pelletter.GetComponentInChildren<Animator>();
        animFlame = flameMeter.GetComponentInChildren<Animator>();
        ammoCounter.canvasRenderer.SetAlpha(0.0f);
        bullets = 1;
    }

    // Update is called once per frame
    void Update()
    {
        x = Screen.width / 2; //In the update function, to keep the point at the center even if the screen is resized.
        y = Screen.height / 2;

        curWeapon = GetComponent<WeaponSwitch>().curWeapon; //The currently active weapon
        isSwapping = GetComponent<WeaponSwitch>().isSwapping; //Is the weapon currently being swapped?
        isDying = GetComponent<PlayerStats>().isDying; //Is the player currently dying?
        superUsed = GetComponent<PlayerStats>().superUsed; //Is the super power being used right now? (Will fire super bullets)
        ammoCounter.text = bullets.ToString("0");

        if (!isSwapping && !isDying)
        {
            if (curWeapon == 1)
            {
                ammoCounter.CrossFadeAlpha(0f, 0.2f, false);
                Punch();
            }

            if (curWeapon == 2)
            {
                ammoCounter.CrossFadeAlpha(1f, 0.2f, false);
                Clockgun();
            }
            if (curWeapon == 3)
            {
                ammoCounter.CrossFadeAlpha(1f, 0.2f, false);
                Pelletter();
            }
            if (curWeapon == 4)
            {
                ammoCounter.CrossFadeAlpha(1f, 0.2f, false);
                Flamethrower();
            }
        }

        if(!assigned)
        {
            if(curWeapon == 2)
            {
                bullets = 6;
                assigned = true;
            }
            if(curWeapon == 3)
            {
                bullets = 5;
                assigned = true;
            }
            if (curWeapon == 4)
            {
                bullets = 100;
                assigned = true;
            }
        }

        if(isSwapping)
        {
            assigned = false;
        }
    }

    //For melee
    void Punch()
    {
        anim.SetBool("ReadyToShoot", false);


        //If mouse0 is pressed, punch
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!superUsed)
            {
                if(!punchWait)
                {
                    punchAudio.Play();
                    punchWait = true;
                }
                
                anim.SetTrigger("PunchT");
                attackCollider.SetActive(true);
            }

            if (superUsed)
            {
                if (!punchWait)
                {
                    punchSPAudio.Play();
                    punchWait = true;
                }
                anim.SetTrigger("PunchT");
                attackSCollider.SetActive(true);
            }
        }

        if (!superUsed)
        {
            superLight.SetActive(false);
        }
        if (superUsed)
        {
            superLight.SetActive(true);
        }

        //If the attack collider is active, deactivate it after some time
        if (attackCollider.activeInHierarchy || attackSCollider.activeInHierarchy)
        {
            timerPunch -= Time.deltaTime;
            if (timerPunch <= 0.0f)
            {
                punchWait = false;
                attackCollider.SetActive(false);
                attackSCollider.SetActive(false);
                timerPunch = 0.5f;
            }
        }
    }

    //For the clockgun
    void Clockgun()
    {
        superLight.SetActive(false);
        //Hold mouse0 to shoot normally
        if (Input.GetKey(KeyCode.Mouse0) && !shot1 && bullets > 0 && !reloading)
        {
            anim.SetTrigger("ShootC");
            //Normal bullets
            if (!superUsed)
            {
                clockgunShot.Play();

                var center = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));  //The center of the screen
                var bullet = Instantiate(bulletN, barrelC.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = center.direction * 300; //Fires a bullet to the center of the screen

                muzzleFlash1.GetComponent<ParticleSystem>().Play();
                muzzleFlash2.GetComponent<ParticleSystem>().Play();
            }
            //Super bullets
            if (superUsed)
            {
                clockgunShotSP.Play();

                var center = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
                var bulletSuper = Instantiate(bulletS, barrelC.position, transform.rotation);
                bulletSuper.GetComponent<Rigidbody>().velocity = center.direction * 300;

                superMuzzleFlash1.GetComponent<ParticleSystem>().Play();
                superMuzzleFlash2.GetComponent<ParticleSystem>().Play();
            }
            shot1 = true;
            bullets -= 1;

            //To turn the gear
            gear.transform.Rotate(0, 0, angle);

            anim.SetBool("ReadyToShoot", true);
        }

        //Clockgun primary fire cooldown
        if (shot1)
        {
            timerClockgunPri -= Time.deltaTime;
            if (timerClockgunPri <= 0.0f)
            {
                shot1 = false;
                timerClockgunPri = 0.5f;
                timerShooting = 1f;
            }
        }

        //Hold mouse1 for alt fire mode
        if (Input.GetKey(KeyCode.Mouse1) && !shot2 && bullets > 0 && !reloading)
        {
            anim.SetTrigger("ShootC");
            //Normal bullets
            if (!superUsed)
            {
                clockgunShot.Play();

                var center = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));  //The center of the screen
                var bullet = Instantiate(bulletF, barrelC.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = center.direction * 300; //Fires a bullet to the center of the screen

                muzzleFlash1.GetComponent<ParticleSystem>().Play();
                muzzleFlash2.GetComponent<ParticleSystem>().Play();
            }
            //Super bullets
            if (superUsed)
            {
                clockgunShotSP.Play();

                var center = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
                var bulletSuper = Instantiate(bulletSF, barrelC.position, transform.rotation);
                bulletSuper.GetComponent<Rigidbody>().velocity = center.direction * 300;

                superMuzzleFlash1.GetComponent<ParticleSystem>().Play();
                superMuzzleFlash2.GetComponent<ParticleSystem>().Play();
            }
            shot2 = true;
            bullets -= 1;

            //To turn the gear
            gear.transform.Rotate(0, 0, angle);

            anim.SetBool("ReadyToShoot", true);
        }

        //Clockgun alt fire cooldown
        if (shot2)
        {
            timerClockgunAlt -= Time.deltaTime;
            if (timerClockgunAlt <= 0.0f)
            {
                shot2 = false;
                timerClockgunAlt = 0.1f;
                timerShooting = 1f;
            }
        }

        //Manual reload
        if (bullets < 6 && bullets > 0 && !reloading)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
                anim.SetBool("Reload", true);
            }
        }
        //Forced reloading
        if (bullets <= 0)
        {
            reloading = true;
            anim.SetBool("Reload", true);
        }
        //Reload time
        if (reloading)
        {
            if(!relSound)
            {
                reloadC.Play();
                relSound = true;
            }

            timerClockgunReload -= Time.deltaTime;
            if (timerClockgunReload <= 0.0f)
            {
                anim.SetBool("Reload", false);
                bullets = 6;
                reloading = false;
                timerClockgunReload = 1f;
                relSound = false;
            }
        }

        //When the time is up, the left arm will stop 'aiming'
        timerShooting -= Time.deltaTime;
        if (timerShooting <= 0.0f)
        {
            anim.SetBool("ReadyToShoot", false);
            timerShooting = 0.2f;
        }
    }

    //For the pelletter
    void Pelletter()
    {
        superLight.SetActive(false);
        //Hold mouse0 to shoot normally
        if (Input.GetKey(KeyCode.Mouse0) && !shot1 && bullets > 0 && !reloading)
        {
            anim.SetTrigger("ShootP");
            animPel.SetTrigger("Shoot");
            //Normal pellets
            if (!superUsed)
            {
                pelShot.Play();

                var bullet = Instantiate(pellet, barrelP.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(barrelP.forward * 80, ForceMode.VelocityChange); //Fires a pellet

                muzzleFlashPel1.GetComponent<ParticleSystem>().Play();
                muzzleFlashPel2.GetComponent<ParticleSystem>().Play();
            }
            //Super pellets
            if (superUsed)
            {
                pelShotSP.Play();

                var bulletSuper = Instantiate(pelletS, barrelP.position, transform.rotation);
                bulletSuper.GetComponent<Rigidbody>().AddForce(barrelP.forward * 80, ForceMode.VelocityChange); //Fires a super pellet

                superMuzzleFlashPel1.GetComponent<ParticleSystem>().Play();
                superMuzzleFlashPel2.GetComponent<ParticleSystem>().Play();
            }
            shot1 = true;
            bullets -= 1;

            anim.SetBool("ReadyToShoot", true);
        }

        //Pelletter primary fire cooldown
        if (shot1)
        {
            timerPelPri -= Time.deltaTime;
            if (timerPelPri <= 0.0f)
            {
                shot1 = false;
                timerPelPri = 0.3f;
                timerShooting = 1f;
            }
        }

        //Hold mouse1 for alt fire mode
        if (Input.GetKey(KeyCode.Mouse1) && !shot2 && bullets > 0 && !reloading)
        {
            anim.SetTrigger("ShootP");
            animPel.SetTrigger("Shoot");
            //Normal bullets
            if (!superUsed)
            {
                pelShot.Play();

                var bullet = Instantiate(pelletSticky, barrelP.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(barrelP.forward * 80, ForceMode.VelocityChange); //Fires a pellet

                muzzleFlashPel1.GetComponent<ParticleSystem>().Play();
                muzzleFlashPel2.GetComponent<ParticleSystem>().Play();
            }
            //Super bullets
            if (superUsed)
            {
                pelShotSP.Play();

                var bulletSuper = Instantiate(pelletSuperSticky, barrelP.position, transform.rotation);
                bulletSuper.GetComponent<Rigidbody>().AddForce(barrelP.forward * 80, ForceMode.VelocityChange); //Fires a pellet

                superMuzzleFlashPel1.GetComponent<ParticleSystem>().Play();
                superMuzzleFlashPel2.GetComponent<ParticleSystem>().Play();
            }
            shot2 = true;
            bullets -= 1;

            anim.SetBool("ReadyToShoot", true);
        }

        //Pelletter alt fire cooldown
        if (shot2)
        {
            timerPelAlt -= Time.deltaTime;
            if (timerPelAlt <= 0.0f)
            {
                shot2 = false;
                timerPelAlt = 1f;
                timerShooting = 1f;
            }
        }

        //Manual reload
        if (bullets < 5 && bullets > 0 && !reloading)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
                anim.SetBool("ReloadP", true);
                animPel.SetBool("Reload", true);
                smokeReloadPel.GetComponent<ParticleSystem>().Play();
            }
        }
        //Forced reloading
        if (bullets <= 0)
        {
            reloading = true;
            anim.SetBool("ReloadP", true);
            animPel.SetBool("Reload", true);
            if(!smoke)
            {
                smokeReloadPel.GetComponent<ParticleSystem>().Play();
                smoke = true;
            }
        }
        //Reload time
        if (reloading)
        {
            if (!relSound)
            {
                reloadP.Play();
                relSound = true;
            }

            timerPelReload -= Time.deltaTime;
            if (timerPelReload <= 0.0f)
            {
                anim.SetBool("ReloadP", false);
                animPel.SetBool("Reload", false);
                bullets = 5;
                reloading = false;
                timerPelReload = 1.5f;
                smoke = false;
                relSound = false;
            }
        }

        //When the time is up, the left arm will stop 'aiming'
        timerShooting -= Time.deltaTime;
        if (timerShooting <= 0.0f)
        {
            anim.SetBool("ReadyToShoot", false);
            timerShooting = 0.2f;
        }
    }

    //For the flamethrower
    void Flamethrower()
    {
        superLight.SetActive(false);
        //Hold mouse0 to fire normally
        if (Input.GetKey(KeyCode.Mouse0) && !shot1 && bullets > 0 && !reloading)
        {
            if(!fireStart)
            {
                animFlame.SetTrigger("FireStart");
                fireStart = true;
            }
            animFlame.SetBool("Firing", true);
            anim.SetBool("ShootF", true);
            //Normal fire
            if (!superUsed)
            {
                if(!isFireSound)
                {
                    fireShotStart.Play();
                    fireShot.Play();
                    isFireSound = true;
                }

                if (!fireParticlePlaying)
                {
                    fireParticle.GetComponent<ParticleSystem>().Play();
                    fireParticlePlaying = true;
                }
                fire.SetActive(true);
            }
            //Super fire
            if (superUsed)
            {
                if (!isFireSound)
                {
                    fireShotStart.Play();
                    fireShotSP.Play();
                    isFireSound = true;
                }

                if (!fireParticlePlaying)
                {
                    superfireParticle.GetComponent<ParticleSystem>().Play();
                    fireParticlePlaying = true;
                }
                superfire.SetActive(true);
            }
            shot1 = true;
            bullets -= 1;

            anim.SetBool("ReadyToShoot", true);
        }
        //Stops the flames
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            fireShot.Stop();
            fireShotSP.Stop();
            isFireSound = false;
            fire.SetActive(false);
            superfire.SetActive(false);
            fireParticle.GetComponent<ParticleSystem>().Stop();
            superfireParticle.GetComponent<ParticleSystem>().Stop();
            fireParticlePlaying = false;
            anim.SetBool("ShootF", false);
            animFlame.SetBool("Firing", false);
            fireStart = false;
        }

        //Flamethrower primary fire cooldown
        if (shot1)
        {
            timerFlamePri -= Time.deltaTime;
            if (timerFlamePri <= 0.0f)
            {
                shot1 = false;
                timerFlamePri = 0.05f;
                timerShooting = 1f;
            }
        }

        //Hold mouse1 for alt fire mode
        if (Input.GetKey(KeyCode.Mouse1) && !shot2 && bullets > 0 && !reloading)
        {
            if (!fireStart)
            {
                animFlame.SetTrigger("FireStart");
                fireStart = true;
            }
            animFlame.SetBool("Firing", true);
            anim.SetTrigger("ShootC");
            //Normal fire
            if (!superUsed)
            {
                fireShotStart.Play();
                fireShotAlt.Play();

                if(!fireParticlePlaying)
                {
                    fireAltParticle.GetComponent<ParticleSystem>().Play();
                    fireParticlePlaying = true;
                }

                if (!firecollider)
                {
                    fireAlt.SetActive(true);
                    firecollider = true;
                }
            }
            //Super fire
            if (superUsed)
            {
                fireShotStart.Play();
                fireShotSPAlt.Play();

                if (!fireParticlePlaying)
                {
                    superfireAltParticle.GetComponent<ParticleSystem>().Play();
                    fireParticlePlaying = true;
                }

                if(!firecollider)
                {
                    superfireAlt.SetActive(true);
                    firecollider = true;
                }
            }
            shot2 = true;
            bullets -= 20;

            anim.SetBool("ReadyToShoot", true);
        }

        //Flamethrower alt fire cooldown
        if (shot2)
        {
            animFlame.SetBool("Firing", false);
            fireStart = false;
            timerFlameAlt -= Time.deltaTime;
            if (timerFlameAlt <= 0.0f)
            {
                fireAltParticle.GetComponent<ParticleSystem>().Stop();
                superfireAltParticle.GetComponent<ParticleSystem>().Stop();
                fireParticlePlaying = false;
                shot2 = false;
                timerFlameAlt = 1.3f;
                timerShooting = 1f;
            }

            timerFlameAltCollider -= Time.deltaTime;
            if (timerFlameAltCollider <= 0.0f)
            {
                fireAlt.SetActive(false);
                superfireAlt.SetActive(false);
                timerFlameAltCollider = 0.15f;
                firecollider = false;
            }
        }

        //Manual reload
        if (bullets < 100 && bullets > 0 && !reloading)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
                anim.SetBool("ReloadF", true);
            }
        }
        //Forced reloading
        if (bullets <= 0)
        {
            bullets = 0;
            reloading = true;
            anim.SetBool("ReloadF", true);
        }
        //Reload time
        if (reloading)
        {
            timerFlameReload -= Time.deltaTime;
            if (timerFlameReload <= 0.0f)
            {
                anim.SetBool("ReloadF", false);
                bullets = 100;
                reloading = false;
                timerFlameReload = 1.5f;
            }
        }

        //When the time is up, the left arm will stop 'aiming'
        timerShooting -= Time.deltaTime;
        if (timerShooting <= 0.0f)
        {
            anim.SetBool("ReadyToShoot", false);
            timerShooting = 0.2f;
        }
    }
}
