using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINeedle : MonoBehaviour
{
    Animator anim;
    public GameObject poofParticle;
    public bool dead;
    bool alreadydead = false;
    bool disappeared = false;
    float timerToDie = 2f;
    float startTime;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        startTime = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        dead = GetComponent<EnemyStats>().isDead;

        if(!dead && !started)
        {
            startTime -= Time.deltaTime;
            if (startTime <= 0.0f)
            {
                anim.SetBool("Fill", true);
                started = true;
            }
        }
        if(dead && !alreadydead)
        {
            anim.SetTrigger("Dead");
            alreadydead = true;
        }

        if(alreadydead && !disappeared)
        {
            timerToDie -= Time.deltaTime;
            if (timerToDie <= 0.0f)
            {
                var deadParticle = Instantiate(poofParticle, transform.position, transform.rotation);
                gameObject.SetActive(false);
                disappeared = true;
            }
        }
    }
}
