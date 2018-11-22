using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Player : MonoBehaviour {

	public Vector2 velocity;
	public Vector2 velocityPre;

	// Gravity Variables
	public float[] gravity; // 0 --> min, 1 --> max
	public float gravityNow;
	private float gravityPre;
	public float addGravity;
	private float midStateTimer = 10f;
	private bool gliding = false;
	
	// Flapping Variables
	public int flapState;
	private int flapStateNow;
	private float mouseChange;
	private float mousePosPre;
	public float flapThreshhold;
	public Vector2 flapForce;
	private float noFlapTimer = 0;
	private float flapTimer = 0;
	private float direction;
	
	// Diving Variables
	private float timeLeftForDiving = 1f;
	public float diveTreshhold;
	private bool diving = false;
	private bool timeToDive = false;
	float mouseChangeTotal = 0;
	private float mouseChangeX;
	private float mousePosPreX;

	public float drag = 0.01f;
	private float degree;
	private float currentYPos;
	private float previousYPos = 0;
	public float velocityMaxY;
	public float velocityMaxX;
	private Vector3 currentRotation;
	private Animator m_Animator;
	private float angle;
	
	// Moveable Area
	private float maxMoveableArea = 1200;
	private float minMoveableArea = -1000;
	
	void Start () {
		m_Animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		velocity += Vector2.down * gravityNow;
		transform.position += (Vector3) velocity;
		BirdRotation();
	}

	void Update () {
		Flying();
		Flapping();
		Diving();
		//MoveableArea();
	}
	
		
	// Get Flying Input
	void Flying()
	{
		float mouseY = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
		mouseChange += mouseY - mousePosPre;
		mousePosPre = mouseY;
		
		if (Mathf.Abs(mouseChange) > flapThreshhold) // we are moving
		{
			// flapforce should go up
			
			// we get faster as we flap
			if (mouseChange < 0 && flapState > 1) // if we're going down and if we weren't down already
			{
				gliding = false;
				noFlapTimer = 0;
				velocity += flapForce;

				if (velocity.x >= velocityMaxX)
				{
					velocity.x = velocityMaxX;
				}

				if (velocity.y >= velocityMaxY)
				{
					velocity.y = velocityMaxY;
				}

				if (velocity.y < 0)
				{
					velocity += flapForce * 2;
				}
			}
			
			// gravity effects us less and less
			gravityNow -= addGravity;
			gravityNow = Mathf.Clamp(gravityNow, gravity[0], gravity[1]);

			// -1 == dive
			// 0 == going down
			// 1 == mid state
			// 2 == going up

			if (diving == false)
			{
				flapState += (int)Mathf.Sign(mouseChange);
				flapState = Mathf.Clamp(flapState, 0, 2);
			}
			gliding = false;
			mouseChange = 0;
		}
		else // if we are not flapping
		{
			noFlapTimer += Time.deltaTime; // keep track of the time without flapping
		
			if (diving)
			{
				gravityNow += addGravity*2f;
				gravityNow = Mathf.Clamp(gravityNow, gravity[0], gravity[1]*3);
			}
			// if we stay on midstate
			else if (flapState == 1) //  if we are gliding
			{
				if (noFlapTimer > .3f)
				{
					gliding = true;
					midStateTimer -= Time.deltaTime;

					if (midStateTimer > 0)
					{
						gravityNow = 0.0005f;
						velocity.y = Mathf.Clamp(velocity.y, 0, 0.05f);
						drag = 0;
					}
					else
					{
						gliding = false;
						gravityNow -= addGravity;
						flapState = 0;
						midStateTimer = 10f;
					}
				}
			}
			else
			{
				gravityNow += addGravity;
				gravityNow = Mathf.Clamp(gravityNow, gravity[0], gravity[1]);
				velocity += Vector2.left * drag;
			}
		}
		gravityPre = gravityNow;
	}

	// Diving Input and Gesture	
	
	void Diving()
	{
		// three steps of diving
		// 1. no dive = when we just flap, mouse hasn't moved beyond the treshhold to the right
		// 2. when I drag the mouse to right above a threshhold, mouse has already passed the threshhold
		// 3. mouse goes beyond the y threshhold
		
		// if the mouse continues to move right, then we 
		float mouseX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
		float treshholdScreenX = Camera.main.ScreenToViewportPoint(gameObject.transform.position).x;
//		Debug.Log("threshhold screen x is "+treshholdScreenX);
		
		if (diving == false)
		{
			mouseChangeX += mouseX - mousePosPreX;
			mousePosPreX = mouseX;
		}

//		Debug.Log("mouse x is "+ mouseX);
//		Debug.Log("mouse x change is "+ mouseChangeX);
		
		if (mouseChangeX > 0.01f) // if we are going up and 
		{
			timeToDive = true;
		}
		else
		{
			mouseChangeX = 0;
		}

		if (timeToDive)
		{
			timeLeftForDiving -= Time.deltaTime;
			if (timeLeftForDiving > 0) // we have time to do the second act to dive 
			{
		
				if (mouseChange < 0) // if we move the mouse downwards
				{
					mouseChangeTotal += mouseChange;
//					Debug.Log("mouse change y is "+mouseChangeTotal);
		
					if (Mathf.Abs(mouseChangeTotal) > diveTreshhold) // if the distance was enough to instantiate diving
					{
						diving = true;
						if (diving)
						{
							flapState = -1;
						}
					}
				}
			}
			else // if we're out of time
			{
				timeToDive = false;
				mouseChangeX = 0;
			}	
		}
		else // no diving state
		{
			timeLeftForDiving = 2f;
			mouseChangeTotal = 0;
		}

		if (diving == true && mouseChange > 0)
		{
			diving = false;
			mouseChangeTotal = 0;
		}
	}
	
	
	void BirdRotation()
	{
		currentYPos = transform.position.y;
		float direction = currentYPos - previousYPos;
		
		if (flapState == 1) // mid state
		{
			degree = 0f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
		}
		else if (flapState == -1) // dive
		{
			degree = -60f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
		}
		else 
		{
			
			if (direction > 0)
			{	
				degree = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
				angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
				currentRotation = transform.eulerAngles;
				currentRotation.z = Mathf.Clamp(currentRotation.z, -7, 7);
				transform.eulerAngles = currentRotation;
			}
			else
			{	
				if (velocity.y < -1.5f)
				{
					diving = true;
				degree = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
				angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
				
				}
			
			}	
		
		}
		previousYPos = currentYPos;
	}
		
	// Animation States
	void Flapping()
	{
		int animationState = flapState;
		if (flapState == -1)
		{
			animationState = 0;
		}

		switch (animationState)
		{
			case 0:
				m_Animator.SetBool("isGoingDown", true);
				m_Animator.SetBool("isGoingMiddle", false);
				m_Animator.SetBool("isGoingUp", false);
				break;
			case 1:
				m_Animator.SetBool("isGoingDown", false);
				m_Animator.SetBool("isGoingMiddle", true);
				m_Animator.SetBool("isGoingUp", false);
				break;
			case 2:
				m_Animator.SetBool("isGoingDown", false);
				m_Animator.SetBool("isGoingMiddle", false);
				m_Animator.SetBool("isGoingUp", true);
				break;
		}

		if (diving)
		{
			m_Animator.SetBool("isDiving" , true);
		}
		else
		{
			m_Animator.SetBool("isDiving" , false);
		}
	}

	// public variables
	public float flightDirection()
	{
		return direction;
	}

	public bool glidingNow()
	{
		return gliding;
	}

	public bool divingNow()
	{
		return diving;
	}

	public float UpperLimit()
	{
		return maxMoveableArea;
	}
	
	public float BottomLimit()
	{
		return minMoveableArea;
	}
}
