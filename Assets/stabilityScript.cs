using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stabilityScript : MonoBehaviour
{
    public bool canRotate;
    public bool canMove;
    public bool isRotating;
    public bool isMoving;

    public Rigidbody2D rb;
    
    public float timer;

    public void assignVariables() {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update() {
        if (rb == null)
        {
            assignVariables();
            return;
        }

        rb.constraints = RigidbodyConstraints2D.None;
        if (!canMove) {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        if (!canRotate) {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }


    }
    private void FixedUpdate() {
        if (rb == null ) 
        {
            return;
        }

        if (timer > 0)
        {
             timer -= Time.fixedDeltaTime;   
        } else {
            applyRandomRotation();
            timer = 5;
        }
    }

    private  void applyRandomRotation() {
        float randomForce = Random.Range(1000.0f, 1250.0f);
        int randomDirection = Random.Range(0, 2);
        if (randomDirection == 0)
        {
            rb.AddForceAtPosition(new Vector2(-randomForce,0), Vector2.right * -1);
            print("left");
        }
        else    
        {
            rb.AddForceAtPosition(new Vector2(randomForce,0), Vector2.right);
            print("right");
        }
        
    }

}
