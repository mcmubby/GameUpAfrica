using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] helixRings;
    public float ySpawn = 0;
    public float ringDistance = 5;

    public int numberOfRings;
    
    void Start()
    {
        numberOfRings = GameManager.currentLevelIndex + 5;

        for (int i = 0; i < numberOfRings-1; i++)
        {
            if (i == 0)
                SpawnRing(0);

            SpawnRing(Random.Range(1, helixRings.Length - 1));
        }

        SpawnRing(helixRings.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRing(int ringIndex)
    {
        GameObject ring = Instantiate(helixRings[ringIndex], transform.up * ySpawn, Quaternion.identity);
        ring.transform.parent = transform;
        ySpawn -= ringDistance;
    }
}
