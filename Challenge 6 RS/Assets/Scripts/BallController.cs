using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 15;

    private bool isTraveling;
    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;

    public int minSwipeRecognition = 500;
    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;

    private Color solveColor;

    private ParticleSystem splash;
    private AudioSource ballAudio;

    void Start()
    {
        solveColor = Random.ColorHSV(0.5f, 1);
        GetComponent<MeshRenderer>().material.color = solveColor;
        splash = GetComponentInChildren<ParticleSystem>();
        splash.GetComponent<Renderer>().sharedMaterial.color = solveColor;
        ballAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (isTraveling)
        {
            rb.velocity = speed * travelDirection;
        }

        //Change tile color on collision
        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.05f);
        int i = 0;

        while (i < hitColliders.Length)
        {
            TileController tile = hitColliders[i].transform.GetComponent<TileController>();
            if (tile && !tile.isColored)
            {
                splash.Play();
                ballAudio.Play();
                tile.ChangeColor(solveColor);
            }
            i++;
        }

        if (nextCollisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollisionPosition) < 1)
            {
                isTraveling = false;
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
            }
        }

        if (isTraveling)
            return;

        if (Input.GetMouseButton(0))
        {
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipePosLastFrame != Vector2.zero)
            {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                if (currentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }

                currentSwipe.Normalize();

                //UP or Down
                if (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }

                if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }

            swipePosLastFrame = swipePosCurrentFrame;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
    }

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 50f))
        {
            nextCollisionPosition = hit.point;
        }

        isTraveling = true;
    }
}
