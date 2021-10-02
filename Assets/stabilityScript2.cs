using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stabilityScript2 : MonoBehaviour
{
     public bool canRotate;
    public bool canMove;
    public bool isRotating;
    public bool isMoving;
    public float timer;

    private void FixedUpdate() {
        if (timer > 0)
        {   
            gameObject.transform.Rotate(Vector3.forward * Time.fixedDeltaTime);
             timer -= Time.fixedDeltaTime;   
        } else {
            timer = 5;
        }
    }

}
