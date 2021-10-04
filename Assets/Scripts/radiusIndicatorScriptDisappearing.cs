using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radiusIndicatorScriptDisappearing : MonoBehaviour
{
    public GameObject mainObject;
    void Start()
    {

    }

    private void Awake()
    {
        //set the circle size to match the mouse detection distance
        gameObject.transform.localScale = Vector3.one * (mainObject.GetComponent<DisappearingPlatform>().mouseDetectDistance + 3);
    }
    // Update is called once per frame
    void Update()
    {
        // this objects position will the main objects position
        transform.position = mainObject.transform.position;
    }
}
