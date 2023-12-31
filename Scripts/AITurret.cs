using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the behaviour of the turret enemies
public class AITurret : MonoBehaviour
{
    public AudioSource shootSound;

    bool shot = false;
    public GameObject muzzleFlash1;
    public GameObject muzzleFlash2;
    public GameObject bulletp;
    public Transform barrel;
    public GameObject explosionParticle;
    public GameObject turret;
    bool isDead;

    // Update is called once per frame
    void Update()
    {
        isDead = turret.GetComponent<EnemyStats>().isDead;

        if (isDead)
        {
            shot = true;
            var explosion = Instantiate(explosionParticle, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !shot)
        {
            shootSound.Play();

            var bullet = Instantiate(bulletp, barrel.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(barrel.forward * 200, ForceMode.VelocityChange);
            shot = true;

            muzzleFlash1.GetComponent<ParticleSystem>().Play();
            muzzleFlash2.GetComponent<ParticleSystem>().Play();

            StartCoroutine("WaitToShoot");
        }
    }

    //Pauses between shots.
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(0.8f);
        shot = false;
    }
}
