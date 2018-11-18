using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

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
				if (noFlapTimer > 1f)
				{
					gliding = true;
					midStateTimer -= Time.deltaTime;

					if (midStateTimer > 0)
					{
						gravityNow = 0.0005f;
						velocity.y = Mathf.Clamp(velocity.y, 0, 0.2f);
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

	/*
	// area player is allowed to move
	void MoveableArea()
	{
		bool down = false;
		if (transform.position.y >= maxMoveableArea)
		{
			diving = true;
		}

		if (transform.position.y <= minMoveableArea)
		{
			down = true;
		}

		if (down)
		{
			StartCoroutine(DownLimitReached());
		}
		
		
		if (transform.eulerAngles.z > 25)
		{
			down = false;
		}
	}
	
	IEnumerator DownLimitReached()
	{
		transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, 20f, Time.deltaTime));
		velocity += flapForce * .5f;
		yield return null;
	}
	*/

	// Diving Input and Gesture	
	void Diving()
	{
		if (mouseChange > 0 && flapState > 0) // if we are going up and 
		{
			timeToDive = true;	
		}
		
		if (timeToDive)
		{
			
			timeLeftForDiving -= Time.deltaTime;
			if (timeLeftForDiving > 0)
			{
				if (mouseChange < 0)
				{
					mouseChangeTotal += mouseChange;
//					Debug.Log("mouse change total is "+Mathf.Abs(mouseChangeTotal));
		
					if (Mathf.Abs(mouseChangeTotal) > diveTreshhold)
					{
//						Debug.Log("Diving Now!");
						diving = true;
						flapState = -1;
					}
				}
			}
			else
			{
				timeToDive = false;
			}	
		}
		else
		{
			timeLeftForDiving = 1.2f;
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
				float newAngle = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
				transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, newAngle, 0.5f));
				currentRotation = transform.eulerAngles;
				currentRotation.z = Mathf.Clamp(currentRotation.z, -7, 7);
				transform.eulerAngles = currentRotation;
			}
			else
			{
				/*
				if (noFlapTimer == 0)
				{
					currentRotation = transform.eulerAngles;
					currentRotation.z = Mathf.Clamp(currentRotation.z, -15, 30);
					transform.eulerAngles = currentRotation;
				}
				*/
				if (noFlapTimer > 1.5)
				{
				degree = -7f;
				transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, degree, Time.deltaTime));
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
