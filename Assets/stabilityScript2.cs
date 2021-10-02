using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class stabilityScript2 : MonoBehaviour
{
    public bool canRotate;
    public bool canMove;

    public bool canScale;
    public bool isRotating;
    public bool isMoving;

    public bool isClicking;
    public float timer;

    public Vector2 erraticTimerRange;
    public Vector2 speedRange;

    public Vector2 scaleRange;

    private Vector3 originalScale;

    private float rotationSpeed;

    private float timing;

    public float mouseDetectDistance;

    private Vector3 screenPos;
    private Vector3 worldPos;

    public GameObject indicatorCircle;

    private void Awake()
    {
        setSpeed();
        originalScale = gameObject.transform.localScale;
        isClicking = false;

    }

    private void Update()
    {
        screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        
        if (Vector2.Distance(worldPos, gameObject.transform.position) < mouseDetectDistance)
        {
            indicatorCircle.SetActive(true);
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isClicking = true;
            }
        }
        else {
            indicatorCircle.SetActive(false);
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isClicking = false;
        }


        if (!isClicking)
        {
            timing = Time.fixedDeltaTime;
        }
        else
        {
            timing = Time.fixedDeltaTime * .01f;
        }
    }
    private void setSpeed()
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
    private void FixedUpdate()
    {
        if (isClicking) {
            transform.position = worldPos;
        }

        if (timer > 0)
        {
            if (canRotate)
            {
                rotation(rotationSpeed);
            }

            if (canMove)
            {

            }

            timer -= timing;
        }

        else
        {
            if (canRotate)
            {
                setSpeed();

            }
            if (canScale)
            {
                scale();
            }
            timer = Random.Range(erraticTimerRange.x, erraticTimerRange.y);
        }
    }
    public void rotation(float rotationSpeed)
    {

        gameObject.transform.Rotate(Vector3.forward * timing * rotationSpeed);
    }
    public void scale()
    {
        gameObject.transform.localScale = originalScale * Random.Range(scaleRange.x, scaleRange.y);
    }



    
}
