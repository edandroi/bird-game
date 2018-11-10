using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


public class FollowMouse : MonoBehaviour {
 
	private Vector3 mousePosition;
	public float moveSpeed = 0.05f;
	public float speed;
	private Vector3 lastMousePos;
	private Quaternion lastRotation;

	// Update is called once per frame
	void Update () {
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
	
		
		// Rotate Towards Direction
		Vector3 dir = Input.mousePosition - Vector3.zero;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

		float mouseDir = transform.position.y - lastMousePos.y;
		
		if (mouseDir > 0.05)
		{
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
//			transform.Rotate(0.0f, 0.0f, -Input.mousePosition.y);
		}
		if (mouseDir <= -0.05)
		{
			transform.rotation = Quaternion.AngleAxis(angle - 90 , Vector3.forward);
		}

		lastMousePos = transform.position;

	}
}
