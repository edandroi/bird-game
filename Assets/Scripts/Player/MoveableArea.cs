using System.Collections;
using System.Collections.Generic;
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
		else
		{
			upLimit = false;
		}
		
		if (transform.position.y <= minMoveableArea)
		{
			downLimit = true;
			Services.Player.downLimitReached = true;
		}
		else
		{
			downLimit = false;
			Services.Player.downLimitReached = false;
		}

		if (upLimit == true || downLimit == true)
		{
			LimitsReached();
		}

	}
	
	void LimitsReached()
	{
		// If we reach the bottom boundaries
		
		float windForce = Services.Player.velocity.y < 0 ? .12f : .05f;
		if (downLimit)
		{
			Services.Player.velocity += Vector2.up * windForce;
			transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, 11f, .1f));
		}
		

		// If we reach the upper boundaries
		if (upLimit)
		{
			//StartCoroutine(UpperLimitReached());
			Services.Player.velocity += Vector2.down * windForce;
			transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, -11f, .1f));
		}
	}
}
