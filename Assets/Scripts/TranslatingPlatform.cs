using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatingPlatform : MonoBehaviour
{
    public Transform playerTransform;
    public Vector2 position1, position2;

    private float timeOfLastTranslation;
    private float currentTranslationTimeLimit;
    public float timeBetweenTranslations;
    public float randomVariationLimit;

    // Keep track of which position the platform is currently at
    private int currentPosition = 0;

    public float platformWidth = 4f;
    public float platformHeight = 1f;
    public float playerHeight = 2f;
    public float playerWidth = 1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = position1;
        timeOfLastTranslation = Time.time;
        currentTranslationTimeLimit = timeBetweenTranslations;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timeOfLastTranslation > currentTranslationTimeLimit)
        {
            if(currentPosition == 0)
            {
                if(IsOnPlatform(position1))
                {
                    playerTransform.SetParent(transform);
                }
                transform.position = position2;
                currentPosition = 1;
                playerTransform.SetParent(null);
            }
            else
            {
                if (IsOnPlatform(position2))
                {
                    playerTransform.SetParent(transform);
                }
                transform.position = position1;
                currentPosition = 0;
                playerTransform.SetParent(null);
            }

            timeOfLastTranslation = Time.time;
            currentTranslationTimeLimit = GetRandomNear(timeBetweenTranslations, randomVariationLimit);
        }
    }

    // Returns a random number in the interval [input - variability, input + variability]
    private float GetRandomNear(float input, float variability)
    {
        return input + Random.Range(-variability, variability);
    }

    private bool IsOnPlatform(Vector2 platformPosition)
    {
        bool overlapsX = (playerTransform.position.x > platformPosition.x - platformWidth / 2 - playerWidth &&
                          playerTransform.position.x < platformPosition.x + platformWidth / 2 + playerWidth);
        bool overlapsY = (playerTransform.position.y - playerHeight / 2 > platformPosition.y &&
                          playerTransform.position.y - playerHeight / 2 < platformPosition.y + platformHeight / 2 + 0.25);
        return overlapsX && overlapsY;
    }
}
