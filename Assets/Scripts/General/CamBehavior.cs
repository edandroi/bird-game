using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehavior : MonoBehaviour {

	public Transform player;

	public float distanceFromPlayer;
	private float zDistance = -10;
	public float zoomEase;
	private bool diving = false;

	private void Update()
	{
		if (Services.Player.glidingNow() )
		{
			zDistance = Mathf.Lerp(transform.position.z, -100, zoomEase);
		}
		else if(Services.Player.divingNow())
		{
        	zDistance = Mathf.Lerp(transform.position.z, -200, zoomEase);
		}
		else
		{
			zDistance = Mathf.Lerp(transform.position.z, -500, zoomEase*2f);
		}
		
		if (Services.Player.velocity.y > 2f)
		{
			diving = true;
		}
         else
		{
			diving = false;
		}
	}

	void FixedUpdate ()
	{
		if (diving)
		{
			transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(distanceFromPlayer, 0, 0), .2f);
			transform.position = new Vector3(transform.position.x, transform.position.y, zDistance);	
		}
		else
		{
			transform.position = new Vector3(player.transform.position.x + distanceFromPlayer, player.transform.position.y, zDistance);
		}

	}
}
