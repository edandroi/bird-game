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
	private float rotationSpeed;

	void Start () {
		//gravity=new float[3];
		m_Animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		velocity += Vector2.down*(gravity[flapState]);
		transform.position += (Vector3)velocity;
		//transform.eulerAngles = new Vector3(0,0,Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));
		BirdRotation();
	}

	void Update () 
	{
		InputFlying();
		Flapping();
		
		Debug.Log("velocity is "+velocity);
	}

	void BirdRotation()
	{
		rotationSpeed = 2;
		
		currentYPos = velocity.y;
		float dir = currentYPos - previousYPos;
		if (dir >= 0 && flapState != 1)
		{
			degree = 35f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime * rotationSpeed);
		} 
		else if (dir < 0 && flapState != 1)
		{
			degree = -45f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
		}
		else
		{
			degree = 0f;
			angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
		}

		previousYPos = currentYPos;

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
			}
			flapState += (int)Mathf.Sign(mouseChange);
			flapState = Mathf.Clamp(flapState, 0, 2);
			
			mouseChange = 0;
		}
		else
		{
			Debug.Log("I'm dragging");
			velocity += Vector2.left * drag;
		}
	}

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
}
