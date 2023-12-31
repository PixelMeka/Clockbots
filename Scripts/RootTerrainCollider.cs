using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTerrainCollider : MonoBehaviour
{
    public GameObject spawnCollider;
    bool spawn = false;
    bool isDead = false;
    Animator anim;
    bool colEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        spawn = spawnCollider.GetComponent<ControllerCollider>().collided;
        isDead = gameObject.GetComponent<EnemyStats>().isDead;

        if(spawn && !colEnabled)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            colEnabled = true;
        }

        if(isDead)
        {
            anim.SetTrigger("Burrow");
            anim.SetBool("Dead", true);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<EnemyStats>().enabled = false;
        }
    }
}
