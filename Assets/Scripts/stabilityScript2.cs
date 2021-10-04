using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class stabilityScript2 : MonoBehaviour
{
    // how an object moves erraticly can be decided in the inspector
    public bool canRotate;
    public bool canScale;
    public bool isClicking; // if object is being clicked
    public float timer;

    public Vector2 erraticTimerRange;
    public Vector2 speedRange;

    public Vector2 scaleRange;

    private Vector3 originalScale;

    private float rotationSpeed;

    private float timing;

    public float mouseDetectDistance;

    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;

    public GameObject indicatorCircle;

    public Collider2D objCollider;

    public Color platformDefaultColor;


    private void Awake()
    {

        setRotationSpeed(); // on awake, set rotation speed for the first time
        originalScale = gameObject.transform.localScale; // save a copy of the original scale
        isClicking = false;
        canRotate = true;
        objCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        
        if (isClicking)
        {
            if (transform.position != mouseWorldPos)
            {
                transform.position = Vector3.Lerp(transform.position, mouseWorldPos, 0.1f);
            }
        }

        if (timer > 0)
        {
            
            if (canRotate)
            {
                
                rotateObject(rotationSpeed); // while the timer hasn't reach 0 and we can rotate, rotate the object
            }
            timer -= timing;
        }

        else
        {
            if (canRotate)
            {
                setRotationSpeed(); // when timer hits 0 set a new rotaton speed
            }
            if (canScale)
            {
                scaleObject();  // when timer hits 0 set a new scale for the object
            }
            timer = Random.Range(erraticTimerRange.x, erraticTimerRange.y);
        }
    }
    private void Update()
    {
        if (!isClicking)
        {
            timing = Time.fixedDeltaTime;
            
        } else {
            timing = Time.fixedDeltaTime * .1f;
            
        }
        if (!PlayerMovement.playerScriptInstance.hasPowers) {
            return;
        }
        // convert mouse pos to world space
        mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        // if mouse is within the specified radius of the object...
        if (Vector2.Distance(mouseWorldPos, gameObject.transform.position) < mouseDetectDistance)
        {
            indicatorCircle.SetActive(true);

            // if object is being right clicked...
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                if (!isClicking) {
                    objCollider.enabled = false;
                } else {
                    objCollider.enabled = true;
                }
                
                // go into slow motion
                canRotate = !canRotate;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                objCollider.enabled = false;
                isClicking = true;   
            }
        } else if (!isClicking){
            indicatorCircle.SetActive(false);
            objCollider.enabled = true;
        }

        if (!canRotate) {
            GetComponent<Renderer>().material.SetColor("_Color", Color.black);  
            if (isClicking) {
                objCollider.enabled = false;
            } else {
                objCollider.enabled = true;
            }
            
        }
 
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GetComponent<Renderer>().material.SetColor("_Color", platformDefaultColor);
            objCollider.enabled = true;
            isClicking = false;
        }

        if (!isClicking && canRotate) {
            GetComponent<Renderer>().material.SetColor("_Color", platformDefaultColor);
            objCollider.enabled = true;
        }
        

    }
    private void setRotationSpeed()
    {

        rotationSpeed = Random.Range(speedRange.x, speedRange.y);
        int direction = Random.Range(0, 2);
        switch (direction)
        {
            case 1:
                rotationSpeed *= 1;
                break;
            case 0:
                rotationSpeed *= -1;
                break;
        }
    }

    public void rotateObject(float rotationSpeed)
    {

        gameObject.transform.Rotate(Vector3.forward * timing * rotationSpeed);
    }
    public void scaleObject()
    {
        gameObject.transform.localScale = originalScale * Random.Range(scaleRange.x, scaleRange.y);
    }




}
