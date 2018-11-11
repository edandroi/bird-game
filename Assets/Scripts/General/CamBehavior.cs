using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehavior : MonoBehaviour {

	public Transform player;

	public float distanceFromPlayer;
	private float zDistance = -10;
	public float zoomEase;

	private void Update()
	{
		if (Services.Player.glidingNow())
		{
			zDistance = Mathf.Lerp(transform.position.z, -7, zoomEase*1.2f);
		}
		else
		{
			zDistance = Mathf.Lerp(transform.position.z, -10, zoomEase);
		}
	}

	void FixedUpdate ()
	{
		transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(distanceFromPlayer, 0, 0), .2f);
		transform.position = new Vector3(transform.position.x, transform.position.y, zDistance);	
	}
}
