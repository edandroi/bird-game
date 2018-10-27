using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabeCam : MonoBehaviour
{
	public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.position = Vector3.Lerp(transform.position, player.position, .2f);
		transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}
}
