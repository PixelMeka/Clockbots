using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootJ : MonoBehaviour
{
    public AudioSource shootSound;

    public Transform barrel1;
    public Transform barrel2;
    public GameObject bulletp;
    public GameObject muzzleFlash1;
    public GameObject muzzleFlash2;
    public GameObject muzzleFlash3;
    public GameObject muzzleFlash4;
    public float force;
    bool aiming;
    public bool shot;

    //For Clockgun
    public GameObject gear;
    public GameObject gear2;
    float angle = 140;
    float angle2 = -140;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        aiming = GetComponent<AIClockbot>().aiming;

        //For the big enemies:
        if (aiming && !shot)
        {
            shootSound.Play();

            var bullet = Instantiate(bulletp, barrel1.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(barrel1.forward * force, ForceMode.VelocityChange);

            var bullet2 = Instantiate(bulletp, barrel2.position, transform.rotation);
            bullet2.GetComponent<Rigidbody>().AddForce(barrel2.forward * force, ForceMode.VelocityChange);
            shot = true;

            muzzleFlash1.GetComponent<ParticleSystem>().Play();
            muzzleFlash2.GetComponent<ParticleSystem>().Play();

            muzzleFlash3.GetComponent<ParticleSystem>().Play();
            muzzleFlash4.GetComponent<ParticleSystem>().Play();

            //To turn the gear
            gear.transform.Rotate(0, 0, angle);
            gear2.transform.Rotate(0, 0, angle2);

            StartCoroutine("WaitToShoot");
        }
    }
    
    //Pauses between shots.
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(0.1f);
        shot = false;
    }
}
