using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radiusIndicatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainObject;
    void Start()
    {
        
    }

    private void Awake() {
        gameObject.transform.localScale = Vector3.one * (mainObject.GetComponent<stabilityScript2>().mouseDetectDistance + 3);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = mainObject.transform.position;
    }
}
