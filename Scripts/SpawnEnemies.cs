using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script lets spawners spawn enemies. It is controlled by an animator event.
public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;

    void Spawn()
    {
        enemy.SetActive(true);
    }

}
