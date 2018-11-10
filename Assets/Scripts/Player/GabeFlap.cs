using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

public class GabeFlap : MonoBehaviour
{
	public Vector2 velocity;
	public Vector2 velocityPre;

	public float[] gravity;
	public float drag = 0.01f;
	public int flapState;
	private float mouseChange;
	private float mousePosPre;
	public float flapThreshhold;
	public Vector2 flapForce;
	private Animator m_Animator;
	
	private float degree;
	private float angle;
	private float currentYPos;
	private float previousYPos = 0;
	public float rotationSpeed;
	public float velocityMaxY;
	public float velocityMaxX;
	private Vector3 currentRotation;


	void Start () 
	{
		m_Animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		velocity += Vector2.down*(gravity[flapState]);
		transform.position += (Vector3)velocity;
		
//		Debug.Log("current velocity is "+velocity);

		BirdRotation();
	}

	void Update () 
	{
		InputFlying();
		Flapping();
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
		else if (flapState == 0) // Going Down State
		{
//			transform.eulerAngles = new Vector3(0,0,Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));

			if (direction > 0) // Bird is moving up
			{
				Vector3 newAngle = new Vector3(0,0,Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));
				transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, newAngle, Time.fixedDeltaTime/2);
				currentRotation = transform.eulerAngles;
				currentRotation.z = Mathf.Clamp(currentRotation.z, -35, 35);
				transform.eulerAngles = currentRotation;
			}
			else
			{
				degree = -35f;
				transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, degree, Time.deltaTime));
			}
		}
		else // flapState == 2, bird is going up
		{
			if (direction > 0) // Bird is moving up
			{
				Vector3 newAngle = new Vector3(0,0,Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));
				transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, newAngle, Time.fixedDeltaTime/2);
				currentRotation = transform.eulerAngles;
				currentRotation.z = Mathf.Clamp(currentRotation.z, -35, 35);
				transform.eulerAngles = currentRotation;
			}
			else
			{
				degree = 35f;
				angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
			}
		}
		previousYPos = currentYPos;
		/*
		currentRotation = transform.eulerAngles;
		currentRotation.z = Mathf.Clamp(currentRotation.z, -45, 35);
		transform.eulerAngles = currentRotation;
		*/


	}

	void InputFlying()
	{
		float mouseY = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
		mouseChange += mouseY - mousePosPre;
		mousePosPre = mouseY;
		if (Mathf.Abs(mouseChange) > flapThreshhold)
		{
			if (mouseChange < 0 && flapState > 0) // if we're going down and if we weren't down already
			{
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
			
			// 0 == going down
			// 1 == mid state
			// 2 == going up
			flapState += (int)Mathf.Sign(mouseChange);
			flapState = Mathf.Clamp(flapState, 0, 2);
			
			mouseChange = 0;
		}
		else
		{
			velocity += Vector2.left * drag;
		}
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

	
	
	void BirdRotation2() // Bu calismiyor cunku hiza gore degil animasyona gore donduruyor
	{
		if (flapState == 1){
			degree = 0f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
		}

		if (flapState == 0)
		{
			degree = -35f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
		}

		if (flapState == 2)
		{
			degree = 35f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
		}
	}

	public bool floatingBird()
	{
		if (flapState == 1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
