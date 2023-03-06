using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{ 
    public GameObject Object;
    public Transform[] spawnPoints;

    private void Start()
    {
        Spawner();
    }

    void Spawner()
    {
        for(int i = 0; i < 10;)
        {
            Instantiate(Object, spawnPoints[i].position, spawnPoints[i].rotation);
            i++;
        }
    }
}


