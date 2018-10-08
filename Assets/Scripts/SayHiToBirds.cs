using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayHiToBirds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Raycasting();
	
	}

	void Raycasting()
	{
		Vector2 startPos = transform.position;
		Vector2 direction = transform.right*20;
		Vector2 raycasted = startPos + direction ;
		Debug.DrawLine(transform.position, raycasted, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, raycasted);
		
		if (Input.GetMouseButtonDown(0))
		{
			if (hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "bird")
				{
					Debug.Log("Hi Bird!");
				}
			}
		}
	}
}
