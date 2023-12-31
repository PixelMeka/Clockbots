using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Checkpoint system
public class Checkpoint : MonoBehaviour
{
    public AudioSource checkPointSound;

    [SerializeField]
    private GameObject checkPoint0;

    [SerializeField]
    private GameObject checkPoint1;

    [SerializeField]
    private GameObject checkPoint2;

    [SerializeField]
    private GameObject checkPoint3;

    [SerializeField]
    private GameObject checkPoint4;

    [SerializeField]
    private GameObject checkPoint5;

    [SerializeField]
    private GameObject checkPoint6;

    [SerializeField]
    private GameObject checkPoint7;

    [SerializeField]
    private GameObject checkPoint8;

    [SerializeField]
    private Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
        checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
        checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
        checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
        checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
        checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
        checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
        checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
        checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ch0")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint0.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Play();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (other.tag == "Ch1")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint1.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Play();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (other.tag == "Ch2")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint2.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Play();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (other.tag == "Ch3")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint3.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Play();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (other.tag == "Ch4")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint4.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Play();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (other.tag == "Ch5")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint5.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Play();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (other.tag == "Ch6")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint6.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Play();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (other.tag == "Ch7")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint7.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Play();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (other.tag == "Ch8")
        {
            checkPointSound.Play();

            spawnPoint.transform.position = checkPoint8.transform.position;
            checkPoint0.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint1.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint2.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint3.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint4.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint5.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint6.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint7.GetComponentInChildren<ParticleSystem>().Stop();
            checkPoint8.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
