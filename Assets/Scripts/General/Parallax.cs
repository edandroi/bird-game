using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	public float moveSpeedPercent;

	private Transform cam;

	private Vector2 prevPos;
	// Use this for initialization
	void Start ()
	{
		cam = Camera.main.transform;
		prevPos = cam.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += (Vector3)(((Vector2) cam.position - prevPos) * moveSpeedPercent);
		prevPos = cam.position;


	}
}
