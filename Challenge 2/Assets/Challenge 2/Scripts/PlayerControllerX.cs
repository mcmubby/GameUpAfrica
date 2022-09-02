using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float spawnDelayCounter;
    private float spawnDelay = 1.5f;

    // Update is called once per frame
    void Update()
    {
        //Increase time every frame
        spawnDelayCounter += Time.deltaTime;

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && spawnDelayCounter > spawnDelay)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            spawnDelayCounter = 0;
        }
    }
}
