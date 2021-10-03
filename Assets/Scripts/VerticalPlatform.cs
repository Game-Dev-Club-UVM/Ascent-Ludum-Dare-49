using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector2D;
	[SerializeField] private float waitTime = 0.5f;
	private float waitTimer;

	void Start()
	{
		effector2D = GetComponent<PlatformEffector2D>();
	}

	void Update()
	{
		if (Input.GetAxis("Vertical") >= 0)
		{
			waitTimer = waitTime;
		}

		if(Input.GetAxis("Vertical") < 0)
		{
			if(waitTimer <= 0)
			{
				effector2D.rotationalOffset = 180f;
				waitTimer = waitTime;
			}
			else
			{
				waitTime -= Time.deltaTime;
			}
		}

		if (Input.GetButton("Jump"))
		{
			effector2D.rotationalOffset = 0f;
		}
	}
}
