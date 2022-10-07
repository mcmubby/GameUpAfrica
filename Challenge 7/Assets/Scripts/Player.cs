using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRB;
    public float bounceForce = 6;
    private AudioManager audioManager;

    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision other) {
        audioManager.Play("bounce");

        playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, playerRB.velocity.z);

        string materialName = other.transform.GetComponent<MeshRenderer>().material.name;
        
        if(materialName == "Safe (Instance)")
        {

        }
        else if(materialName == "Unsafe (Instance)")
        {
            audioManager.Play("game over");
            GameManager.gameOver = true;
        }
        else if(materialName == "LastRing (Instance)" && !GameManager.levelCompleted)
        {
            audioManager.Play("win level");
            GameManager.levelCompleted = true;
        }
    }
}
