using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class stabilityScript2 : MonoBehaviour
{
    // how an object moves erraticly can be decided in the inspector
    public bool canRotate;
    public bool canMove;
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

    private void Awake()
    {
        setRotationSpeed(); // on awake, set rotation speed for the first time
        originalScale = gameObject.transform.localScale; // save a copy of the original scale
        isClicking = false;

    }

    private void FixedUpdate()
    {
        if (isClicking)
        {
            if (transform.position != mouseWorldPos)
            {
                transform.position = Vector3.Lerp(transform.position, mouseWorldPos, 0.1f);
            }
            //transform.position = mouseWorldPos; // if object being clicked, position = mouse position
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
        mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        if (Vector2.Distance(mouseWorldPos, gameObject.transform.position) < mouseDetectDistance)
        {
            indicatorCircle.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                isClicking = true;
            }
        } else if (!isClicking) {
            indicatorCircle.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            isClicking = false;
        }


        if (!isClicking)
        {
            timing = Time.fixedDeltaTime;
        }
        else
        {
            timing = Time.fixedDeltaTime * .1f;
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
