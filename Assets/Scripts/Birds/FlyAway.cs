using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour
{
	public Vector3 velocity;

	private float degree;

	private float angle;

	public float speed;

	private void FixedUpdate()
	{
		velocity = new Vector2(-1,1) * speed;
		transform.position += (Vector3) velocity;
		Vector3 m_Rotation = transform.eulerAngles;
		m_Rotation.z = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
	}
}
