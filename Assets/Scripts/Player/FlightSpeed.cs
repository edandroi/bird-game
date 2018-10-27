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
	private float speedUp;
	public float speedUpEase;
	public float slowDownMax;
	private float slowDown;
	public float slowDownEase;
	public float maxSpeed;
	public float minSpeed;

	public float flapRangeMax;
	public float flapRangeMin;

	private float direction;

	private float moveTreshhold;

	private float timer;
	public AnimationCurve speedUpCurve;
	public AnimationCurve slowDownCurve;

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
			
			moveTreshhold = currentPos.y - lastPos.y;
			if ((moveTreshhold) > 0) // moving up
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
						speedUp = speedUpCurve.Evaluate(flightSpeed);
						flightSpeed += speedUp;
						//speedUp += speedUpEase;
						//Debug.Log("speed up is ="+speedUp);
					}
					else
					{
						flightSpeed = maxSpeed;
					}
				}
			}
		
			if (moveUpNow == moveUpPre)
			{				
				if (flightSpeed > minSpeed)
				{
					if (slowDown < slowDownMax)
					{
						slowDown = slowDownCurve.Evaluate(flightSpeed);
						flightSpeed -= slowDown/2;
//						Debug.Log("slow down is ="+slowDown);
						//slowDown += slowDownEase;
					}
					else
					{
						flightSpeed -= slowDownMax;
					}
				}
				else
				{
					flightSpeed = minSpeed;
				}
			}
	}

	/*
	private IEnumerator SlowDown()
	{
		timer -= Time.deltaTime;
		if (flightSpeed > minSpeed)
		{
			if (timer <= 0)
			{
				if (slowDown < slowDownMax)
				{
					flightSpeed -= slowDown;
					slowDown += slowDownEase;
					yield return null;
				}
				else
				{
					flightSpeed -= slowDownMax;
					yield return null;
				}
			}
		}
	}
	*/

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

	public float MoveDirection()
	{
		return moveTreshhold;
	}
}
                                                                                                                                                                                                                                                                                                                                                                                                                                            