using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class FlightSpeed : MonoBehaviour {

	Vector3 mousePos;

	private Vector3 currentPos;
	private Vector3 lastPos = Vector3.zero;
	private Vector3 previousPos = Vector3.zero;

	private bool moveUpNow;
	private bool moveUpPre;

	private Vector3 turningPosCurrent = Vector3.zero;
	private Vector3 turningPosLast = Vector3.zero;

	private float turningPointDis;

	public float flightSpeed = 0;
	public float speedUp;
	public float speedUpEase;
	public float slowDownMax;
	public float slowDown;
	public float slowDownEase;
	public float maxSpeed;
	public float minSpeed;

	public float flapRangeMax;
	public float flapRangeMin;

	private float direction;

	void Update()
	{
		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		currentPos = mousePos;
		
		CalculateDirections();
		CalculateFlightSpeed();
		
		
		lastPos = currentPos;
		moveUpPre = moveUpNow;

//		Debug.Log("fligth speed is " + Services.FlightSpeed.flightSpeed);
	}

	// FUNCTIONS
	void CalculateDirections()
	{
		if (mousePos.y < flapRangeMax && mousePos.y > flapRangeMin)
		{
			if ((currentPos.y - lastPos.y) > 0) // moving up
			{
				moveUpNow = true;
			}
			else
			{
				moveUpNow = false;
			}
		}
	}

	void CalculateFlightSpeed()
	{
			// Compare the lastest TP with the last TP
			if (moveUpNow != moveUpPre)
			{
				turningPosLast = turningPosCurrent;
				turningPosCurrent = currentPos;
				turningPointDis = Mathf.Abs(turningPosCurrent.y - turningPosLast.y);
				if (Input.GetMouseButton(0)){
					if (flightSpeed < maxSpeed)
					{
						flightSpeed += speedUp;
						speedUp += speedUpEase;
					}
				}
			}
		
			if (moveUpNow == moveUpPre)
			{
				if (flightSpeed > minSpeed)
				{
					if (slowDown < slowDownMax)
					{
						flightSpeed -= slowDown;
						slowDown += slowDownEase;
					}
	
					else
					{
						flightSpeed -= slowDownMax;
					}
				}
			}

	}

	// BOOLS
	public bool MovingUp()
	{
		return moveUpNow;
	}

	// VALUES
	public float PlayerFlightSpeed()
	{
		return flightSpeed;
	}
}
                                                                                                                                                                                                                                                                                                                                                                                                                                            