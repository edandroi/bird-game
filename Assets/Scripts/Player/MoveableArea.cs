using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class MoveableArea : MonoBehaviour
{
	bool downLimit = false;
	bool upLimit = false;
	// Moveable Area
	private float maxMoveableArea = 1200;
	private float minMoveableArea = -1100;
	
	void Update () 
	{
		if (transform.position.y >= maxMoveableArea)
		{
			upLimit = true;
		}
		
		if (transform.position.y <= minMoveableArea)
		{
			downLimit = true;
		}

		if (upLimit == true || downLimit == true)
		{
			LimitsReached();
		}

	}
	
	void LimitsReached()
	{
		// If we reach the bottom boundaries
		if (downLimit)
		{
			StartCoroutine(DownLimitReached());
		}
		

		// If we reach the upper boundaries
		if (upLimit)
		{
			StartCoroutine(UpperLimitReached());
		}
	}
	
	IEnumerator DownLimitReached()
	{
		for (float windForce = .03f; windForce >= 0; windForce -=0.01f)
		{
			Services.Player.velocity += Vector2.up * windForce;
			transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, 10f, .1f));
			yield return downLimit = false;
		}
	}
	
	IEnumerator UpperLimitReached()
	{
		for (float windForce = .06f; windForce >= 0; windForce -=0.01f)
		{
			Services.Player.velocity += Vector2.down * windForce;
			transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, -11f, .1f));
			yield return upLimit = false;
		}
	}	
}
