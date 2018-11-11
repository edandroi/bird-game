using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

// YEDEK YEDEK YEDEK
public class Player1 : MonoBehaviour {

	public Vector2 velocity;
	public Vector2 velocityPre;

	// Gravity Variables
	public float[] gravity; // 0 --> min, 1 --> max
	public float gravityNow;
	private float gravityPre = 0;
	public float addGravity;
	private float midStateTimer = 5f;
	
	// Flapping Variables
	public int flapState;
	private float mouseChange;
	private float mousePosPre;
	public float flapThreshhold;
	public Vector2 flapForce;
	private float noFlapTimer = 0;
	private float flapTimer = 0;

	public float drag = 0.01f;
	private float degree;
	private float angle;
	private float currentYPos;
	private float previousYPos = 0;
	public float rotationSpeed;
	public float velocityMaxY;
	public float velocityMaxX;
	private Vector3 currentRotation;
	private Animator m_Animator;
	
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
		InputFlying();
		Flapping();
		Debug.Log(velocity);
	}
	
	// Get Flying Input
	void InputFlying()
	{
		float mouseY = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
		mouseChange += mouseY - mousePosPre;
		mousePosPre = mouseY;
		
		if (Mathf.Abs(mouseChange) > flapThreshhold) // we are moving
		{
			// flapforce should go up
			// gravity should go down
			// flapstate should change
			
			// we get faster
			if (mouseChange < 0 && flapState > 0) // if we're going down and if we weren't down already
			{
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
			}
			
			// gravity effects us less and less
			gravityNow -= addGravity;
			gravityNow = Mathf.Clamp(gravityNow, gravity[0], gravity[1]);

			// 0 == going down
			// 1 == mid state
			// 2 == going up
			flapState += (int)Mathf.Sign(mouseChange);
			flapState = Mathf.Clamp(flapState, 0, 2);
			
			mouseChange = 0;
		}
		else // if we are not flapping
		{
			noFlapTimer += Time.deltaTime; // keep track of the time without flapping
			
			gravityNow += addGravity;
			gravityNow = Mathf.Clamp(gravityNow, gravity[0], gravity[1]);
			velocity += Vector2.left * drag;
			
			// if we stay on midstate
			if (flapState == 1)
			{
				midStateTimer -= Time.deltaTime;

				if (midStateTimer > 0)
				{
					gravityNow = 0.0005f;
					drag = 0;
				}
				else
				{
					gravityNow -= addGravity;
					flapState = Mathf.Clamp(flapState, 0, 2);
				}
			}
		}

		gravityPre = gravityNow;
	}
	
	// Animation States
	void Flapping() 
	{
		switch (flapState)
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
		else 
		{
			
			if (direction > 0)
			{
				float newAngle = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
				transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, newAngle, 0.5f));
				currentRotation = transform.eulerAngles;
				currentRotation.z = Mathf.Clamp(currentRotation.z, -35, 35);
				transform.eulerAngles = currentRotation;
			}
			else
			{
				if (noFlapTimer > 1.5)
				{
				degree = -35f;
				transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, degree, Time.deltaTime));
				}
			}			
		}
		previousYPos = currentYPos;
	}
	
	void BirdRotation2()
	{
		currentYPos = transform.position.y;
		float direction = currentYPos - previousYPos;
		
		if (flapState == 1) // mid state
		{
			degree = 0f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
		}
		else 
		{
			
			if (gravityPre < gravityNow)
			{
				float newAngle = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
				transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, newAngle, 0.005f));
				currentRotation = transform.eulerAngles;
				currentRotation.z = Mathf.Clamp(currentRotation.z, -35, 35);
				transform.eulerAngles = currentRotation;
			}
			else
			{
				if (noFlapTimer > 1.5)
				{
					degree = -35f;
					transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, degree, Time.deltaTime));
				}
			}			
		}
		previousYPos = currentYPos;
	}
}
