using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightSpeed : MonoBehaviour {

	Vector3 mousePos = Vector3.zero;
	Vector3 lastPos = Vector3.zero;
	public float flightSpeed;

	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		mousePos = Input.mousePosition;
		rb.MovePosition(mousePos * Time.fixedDeltaTime);
		flightSpeed = (transform.position - lastPos).magnitude;
		lastPos = transform.position;
		Debug.Log("velocity is "+ flightSpeed);
	}
}