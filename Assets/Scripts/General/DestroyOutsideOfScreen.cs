using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class DestroyOutsideOfScreen : MonoBehaviour
{

	public float timer;

	void Update ()
	{

		timer -= Time.deltaTime;

		if (timer < 0)
		{
			if (Camera.main.WorldToViewportPoint(transform.position).x < -1 ||
			    Camera.main.WorldToViewportPoint(transform.position).x > 2 ||
			    Camera.main.WorldToViewportPoint(transform.position).y > 2 ||
			    Camera.main.WorldToViewportPoint(transform.position).y < -1)
			{
				Destroy(gameObject);
			}
		}
	}	
}
