using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformStatus { Active, Warning, Inactive };

public class DisappearingPlatform : MonoBehaviour
{
    private PlatformStatus status = PlatformStatus.Active;
    private float timeOfLastSwitch;
    private float timeOfLastFlash;
    private float currentStateTimeLimit;
    private bool textureEnabled = true;

    // Parameters controlling the behavior
    public float warningTimeLimit = 2.0f;
    public float activeTimeLimit = 6.0f;
    public float inactiveTimeLimit = 5.0f;
    public float textureFlashTime = 0.25f;
    public float randomVariationLimit = 0.5f;

    public Material normalMat;
    public Material transparentMat;


    // Start is called before the first frame update
    void Start()
    {
        timeOfLastSwitch = Time.time;
        currentStateTimeLimit =  activeTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        // Switch states
        if(Time.time - timeOfLastSwitch > currentStateTimeLimit)
        {
            if(status == PlatformStatus.Active)
            {
                status = PlatformStatus.Warning;
                currentStateTimeLimit = GetRandomNear(warningTimeLimit, randomVariationLimit);
                timeOfLastFlash = Time.time;
                this.GetComponent<Renderer>().material = normalMat;
            }
            else if(status == PlatformStatus.Warning)
            {
                status = PlatformStatus.Inactive;
                currentStateTimeLimit = GetRandomNear(inactiveTimeLimit, randomVariationLimit);
                this.GetComponent<Renderer>().material = transparentMat;
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                status = PlatformStatus.Active;
                currentStateTimeLimit = GetRandomNear(activeTimeLimit, randomVariationLimit);
                this.GetComponent<Renderer>().material = normalMat;
                this.GetComponent<BoxCollider2D>().enabled = true;
            }

            timeOfLastSwitch = Time.time;
        }

        // Toggle the texture, if relevant
        if(status == PlatformStatus.Warning)
        {
            if(Time.time - timeOfLastFlash > textureFlashTime)
            {
                if(textureEnabled)
                {
                    textureEnabled = false;
                    this.GetComponent<Renderer>().material = transparentMat;
                }
                else
                {
                    textureEnabled = true;
                    this.GetComponent<Renderer>().material = normalMat;
                }
                timeOfLastFlash = Time.time;
            }
        }
    }

    // Returns a random number in the interval [float - variability, float + variability]
    private float GetRandomNear(float input, float variability)
    {
        return input + Random.Range(-variability, variability);
    }
}
