using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 0.004f;

    
    void Update()
    {
        if(!GameManager.isGameStarted)
            return;
        // if (Input.GetMouseButton(0))
        // {
        //     float mouseX = Input.GetAxisRaw("Mouse X");
        //     transform.Rotate(0, -mouseX * rotationSpeed * Time.deltaTime, 0);
        // }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float xTouch = Input.GetTouch(0).deltaPosition.x;
            transform.Rotate(0, -xTouch * rotationSpeed * Time.deltaTime, 0);
        }
    }
}
