using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabeCam : MonoBehaviour
{
	public Transform player;

	public float distanceFromPlayer;


	
	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(distanceFromPlayer, 0, 0), .2f);
		transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}
}
