using System;
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
	
	// Flapping states
	/*
	private bool midToUp;
	private bool midToDown;
	private bool downToMid;
	private bool upToMid;
	*/
	/*
	private bool upPos;
	private bool downPos;
	private bool midPos;
	*/
	
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
	public float velocityMinY;
	public float velocityMaxX;
	private Vector3 currentRotation;
	private Animator m_Animator;
	private float angle;
	public bool downLimitReached = false;
	public bool upLimitReached = false;
	void Start () {
		m_Animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		velocity += Vector2.down * gravityNow;
		transform.position += (Vector3) velocity;
		BirdRotation();
	}

	void Update () 
	{
//		Debug.Log("flap state is "+flapState);
		Flying();
		Flapping();
		Diving();
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
						midStateTimer = 12f;
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

		if (upLimitReached)
		{
			flapState = 2;
		}


		gravityPre = gravityNow;
		velocity.y = Mathf.Clamp(velocity.y, velocityMinY, velocityMaxY);
	}

	// Diving Input and Gesture	
		
	enum DivingState { Horizontal, Vertical, Diving}

	private float mouseChangeY;
	
	private DivingState currentDiveState = DivingState.Horizontal;
	
	void Diving()
	{	
		// Use Enum -  integers
		// great for store different states
		
		switch (currentDiveState)
		{
				case DivingState.Horizontal:
					diving = false;
					timeToDive = false;
					timeLeftForDiving = 2f;
					mouseChangeY = 0;
					break;
				
				case DivingState.Vertical:
					diving = false;
					timeToDive = true;
					break;
				
				case DivingState.Diving:
					flapState = -1;
					diving = true;
					timeToDive = false;
					break;		
		}
		
		// we track the mouse movement on x axis
		float mouseX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
		float treshholdScreenX = Camera.main.ScreenToViewportPoint(gameObject.transform.position).x;
		
		// eger horizontal bolgedeysek

//		Debug.Log("mouse change x is "+ mouseChangeX);
//		Debug.Log("mouse change y is "+ mouseChangeY);
		
		if(mouseX > 0)
		{
		mouseChangeX += mouseX - mousePosPreX;
		mousePosPreX = mouseX;
		}	
		
		if (mouseChangeX > .5)
		{
			currentDiveState = DivingState.Vertical;
//			Debug.Log("mouse change x is "+ mouseChangeX);
		}

		if (timeToDive) 
		{
			timeLeftForDiving -= Time.deltaTime;
			if (timeLeftForDiving > 0) // we have time to do the second act to dive 
			{
				if (mouseChange < 0) // if we move the mouse downwards
				{
					mouseChangeY += mouseChange;
		
					if (mouseChangeY < diveTreshhold) // if the distance was enough to instantiate diving
					{
						currentDiveState = DivingState.Diving;
					}
				}
			}
			else // if we're out of time
			{
				currentDiveState = DivingState.Horizontal;
			}	
		}

		// we're not diving anymore
		if (diving && mouseChange > 0)
		{
			currentDiveState = DivingState.Horizontal;
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
		else // if we are not diving or gliding
		{
			if (direction > 0 )
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
				degree = 0;
				angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
			}	
		
		}
		previousYPos = currentYPos;
	}
		
	// Animation States
	private int animationStates;
	private int preState;

	enum BirdState
	{
		diving,
		gliding,
		goingUpToMid,
		goingMidToUp,
		isUp,
		goingDownToMid,
		goingMidToDown,
		isDown,
		isMiddle,
	}

	private BirdState currentBirdState;

	void Flapping()
	{
		// -1 == dive
		// 0 == going down
		// 1 == mid state
		// 2 == going up

		int animationState = flapState + 1;
		
		switch (animationState) // Determine animation state
		
		{
			case 0: // diving
					currentBirdState = BirdState.diving;
				break;
			
			case 1: // mouse going down

				if (preState == 2) // if we are coming from middle
					currentBirdState = BirdState.goingMidToDown;

				if (preState == 1) // if we were already down
					currentBirdState = BirdState.isDown;
				
				break;
			
			case 2: // mouse going mid
				
				if (preState == 1)
					currentBirdState = BirdState.goingUpToMid;

				if (preState == 3)
					currentBirdState = BirdState.goingDownToMid;

				if (preState == 2)
					currentBirdState = BirdState.isMiddle;
				break;
			
			case 3: // mouse going up 

				if (preState == 2)
				currentBirdState = BirdState.goingMidToUp;

				if (preState == 3)
					currentBirdState = BirdState.isUp;
				
				break;
		}
		
		preState = animationState;

		switch (currentBirdState)
		{
			
			case BirdState.isDown:
				m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , false);
				m_Animator.SetBool("isGoingUpToMid" , false);
				m_Animator.SetBool("isGoingDownToMid" , false);
				m_Animator.SetBool("isDiving" , false);
				m_Animator.SetBool("isGliding" , false);
				m_Animator.SetBool("isDown" , true);
				m_Animator.SetBool("isMiddle", false);
				m_Animator.SetBool("isUp", false);
				m_Animator.SetBool("toDive", false);
				break;
				
			case BirdState.goingDownToMid:
				m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , false);
				m_Animator.SetBool("isGoingUpToMid" , false);
				m_Animator.SetBool("isGoingDownToMid" , true);
				m_Animator.SetBool("isDiving" , false);
				m_Animator.SetBool("isGliding" , false);
				m_Animator.SetBool("isDown" , diving);
				m_Animator.SetBool("isMiddle", true);
				m_Animator.SetBool("isUp", false);
				m_Animator.SetBool("toDive", false);
				break;
				
			case BirdState.isMiddle:
				m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , false);
				m_Animator.SetBool("isGoingUpToMid" , false);
				m_Animator.SetBool("isGoingDownToMid" , false);
				m_Animator.SetBool("isDiving" , false);
				m_Animator.SetBool("isGliding" , false);
				m_Animator.SetBool("isDown" , false);
				m_Animator.SetBool("isMiddle", true);
				m_Animator.SetBool("isUp", false);
				m_Animator.SetBool("toDive", false);
				break;
			
			case BirdState.goingMidToUp:
				m_Animator.SetBool("isGoingMidToUp" , true);
				m_Animator.SetBool("isGoingMidToDown" , false);
				m_Animator.SetBool("isGoingUpToMid" , false);
				m_Animator.SetBool("isGoingDownToMid" , false);
				m_Animator.SetBool("isDiving" , false);
				m_Animator.SetBool("isGliding" , false);
				m_Animator.SetBool("isDown" , false);
				m_Animator.SetBool("isMiddle", false);
				m_Animator.SetBool("isUp", false);
				m_Animator.SetBool("toDive", false);
				break;
			
			case BirdState.isUp:
				m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , false);
				m_Animator.SetBool("isGoingUpToMid" , false);
				m_Animator.SetBool("isGoingDownToMid" , false);
				m_Animator.SetBool("isDiving" , false);
				m_Animator.SetBool("isGliding" , false);
				m_Animator.SetBool("isDown" , false);
				m_Animator.SetBool("isMiddle", false);
				m_Animator.SetBool("isUp", true);
				m_Animator.SetBool("toDive", false);
				break;
			
			case BirdState.goingUpToMid:
				m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , false);
				m_Animator.SetBool("isGoingUpToMid" , true);
				m_Animator.SetBool("isGoingDownToMid" , false);
				m_Animator.SetBool("isDiving" , false);
				m_Animator.SetBool("isGliding" , false);
				m_Animator.SetBool("isDown" , false);
				m_Animator.SetBool("isMiddle", true);
				m_Animator.SetBool("isUp", false);
				m_Animator.SetBool("toDive", false);
				break;
			
			case BirdState.goingMidToDown:
				m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , true);
				m_Animator.SetBool("isGoingUpToMid" , false);
				m_Animator.SetBool("isGoingDownToMid" , false);
				m_Animator.SetBool("isDiving" , false);
				m_Animator.SetBool("isGliding" , false);
				m_Animator.SetBool("isDown" , false);
				m_Animator.SetBool("isMiddle", false);
				m_Animator.SetBool("isUp", false);
				break;
			
			case BirdState.diving:
				m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , false);
				m_Animator.SetBool("isGoingUpToMid" , false);
				m_Animator.SetBool("isGoingDownToMid" , false);
				m_Animator.SetBool("isDiving" , true);
				m_Animator.SetBool("isGliding" , false);
				m_Animator.SetBool("isDown" , false);
				m_Animator.SetBool("isMiddle", false);
				m_Animator.SetBool("isUp", false);
				m_Animator.SetTrigger("toDiving");

				if (diving)
				{
					m_Animator.ResetTrigger("toDiving");
				}

				break;

			
			case BirdState.gliding:
				m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , false);
				m_Animator.SetBool("isGoingUpToMid" , false);
				m_Animator.SetBool("isGoingDownToMid" , false);
				m_Animator.SetBool("isDiving" , false);
				m_Animator.SetBool("isGliding" , true);
				m_Animator.SetBool("isDown" , false);
				m_Animator.SetBool("isMiddle", false);
				m_Animator.SetBool("isUp", false);
				m_Animator.SetBool("toDive", false);
				break;
		}
		


	

		
		
		/*
		if (diving)
		{
			animationStates = 0;
		}

		if (flapState == 1 && direction < 0) // mid to down
		{
			m_Animator.SetBool("isGoingMidToUp" , false);
				m_Animator.SetBool("isGoingMidToDown" , true);
			m_Animator.SetBool("isGoingUpToMid" , false);
			m_Animator.SetBool("isGoingDownToMid" , false);
			m_Animator.SetBool("isDiving" , false);
			m_Animator.SetBool("isGliding" , false);
		}
		
		if (flapState == 1 && direction >= 0) // mid to up
		{
				m_Animator.SetBool("isGoingMidToUp" , true);
			m_Animator.SetBool("isGoingMidToDown" , false);
			m_Animator.SetBool("isGoingUpToMid" , false);
			m_Animator.SetBool("isGoingDownToMid" , false);
			m_Animator.SetBool("isDiving" , false);
			m_Animator.SetBool("isGliding" , false);
		}
		
		if (flapState == 0 && direction >= 0) // down to mid
		{
			m_Animator.SetBool("isGoingMidToUp" , false);
			m_Animator.SetBool("isGoingMidToDown" , false);
			m_Animator.SetBool("isGoingUpToMid" , false);
			m_Animator.SetBool("isGoingDownToMid" , true);
			m_Animator.SetBool("isDiving" , false);
			m_Animator.SetBool("isGliding" , false);
		}
		
		if (flapState == 1 && direction < 0) // up to mid
		{
			m_Animator.SetBool("isGoingMidToUp" , false);
			m_Animator.SetBool("isGoingMidToDown" , false);
			m_Animator.SetBool("isGoingUpToMid" , true);
			m_Animator.SetBool("isGoingDownToMid" , false);
			m_Animator.SetBool("isDiving" , false);
			m_Animator.SetBool("isGliding" , false);
		}

		if (gliding)
		{
			animationStates = 2;
		}
		*/


	}

	void Flapping2()
	{
		//all the states should be accessible immediately
		// any state
		int animationState = flapState + 1;

		switch (animationState)
		{
			case 0:
				m_Animator.SetBool("isDiving" , true);
				m_Animator.SetBool("isGoingUp", false);
				m_Animator.SetBool("isGoingDown", false);
				m_Animator.SetBool("isGoingMiddle", false);
				break;
			case 1:
				m_Animator.SetBool("isGoingDown", true);
				m_Animator.SetBool("isGoingMiddle", false);
				m_Animator.SetBool("isGoingUp", false);
				m_Animator.SetBool("isDiving" , false);
				break;
			case 2:
				m_Animator.SetBool("isGoingDown", false);
				m_Animator.SetBool("isGoingMiddle", true);
				m_Animator.SetBool("isGoingUp", false);
				m_Animator.SetBool("isDiving" , false);
				break;
			case 3:
				m_Animator.SetBool("isGoingDown", false);
				m_Animator.SetBool("isGoingMiddle", false);
				m_Animator.SetBool("isGoingUp", true);
				m_Animator.SetBool("isDiving" , false);
				break;
		}

		if (downLimitReached)
		{
			m_Animator.SetBool("isDiving" , false);
			m_Animator.SetBool("isGoingDown", true);
			
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
}
