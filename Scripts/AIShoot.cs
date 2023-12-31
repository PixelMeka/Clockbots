using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShoot : MonoBehaviour
{
    public AudioSource shootSound;

    public int enemyType; //1 for Clockbot, 2 for Turnip
    public Transform barrel;
    public GameObject bulletp;
    public GameObject muzzleFlash1;
    public GameObject muzzleFlash2;
    public float force;
    bool aiming;
    bool ready;
    public bool shot;
    bool shootNow;
    bool alreadyShot = false;

    //For Clockgun
    public GameObject gear;
    float angle = 140;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyType == 1)
        {
            aiming = GetComponent<AIClockbot>().aiming;
            Clockbot();
        }

        if(enemyType == 2)
        {
            ready = GetComponent<AITurnip>().ready;
            shootNow = GetComponent<AITurnip>().shootNow;
            Turnip();
        }
    }

    //For the shooter enemies:
    void Clockbot()
    {
        if (aiming && !shot)
        {
            shootSound.Play();

            var bullet = Instantiate(bulletp, barrel.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(barrel.forward * force, ForceMode.VelocityChange);
            shot = true;

            muzzleFlash1.GetComponent<ParticleSystem>().Play();
            muzzleFlash2.GetComponent<ParticleSystem>().Play();

            //To turn the gear
            gear.transform.Rotate(0, 0, angle);

            StartCoroutine("WaitToShoot");
        }
    }

    void Turnip()
    {
        if (ready && !shot)
        {
            shot = true;
        }

        if(shootNow && !alreadyShot)
        {
            shootSound.Play();

            var bullet = Instantiate(bulletp, barrel.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(barrel.right * force, ForceMode.VelocityChange);

            muzzleFlash1.GetComponent<ParticleSystem>().Play();
            muzzleFlash2.GetComponent<ParticleSystem>().Play();
            alreadyShot = true;

            StartCoroutine("WaitToShootT");
        }
    }

    //Pauses between shots.
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(1.5f);
        shot = false;
    }

    IEnumerator WaitToShootT()
    {
        yield return new WaitForSeconds(4f);
        shot = false;
        alreadyShot = false;
    }
}
