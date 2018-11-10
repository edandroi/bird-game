using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBird : MonoBehaviour
{
	
	// Sprites
	public Sprite birdUp;
	public Sprite birdDown;
	
	private SpriteRenderer m_SpriteRenderer;
	private Color m_NewColor;

	private float flightSpeedNow;
	private float flightSpeedPre;
	
	private float degree;
	private float angle;

	private float currentPos;
	private float previousPos;

	private Animator m_Animator;

	private float currentYPos;
	private float preYPos = 0;
	
	void Start () 
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_Animator = GetComponent<Animator>();
	}
	

	void Update ()
	{
		MoveableArea(); // The screen space that the player is allowed to move
		PlayerMovement(); // Controls how the player moves
		PlayerAnimation(); // Player Animation based on mouse movement
		PlayerRotation(); // Controls player rotation
	}

	
	// FUNCTIONS

	void PlayerAnimation()
	{
		currentPos = transform.position.y;
		float movingDir = currentPos - preYPos;
		
		if(Input.GetMouseButton(0))
		{
			/*
			//Change Sprites
			if (Input.GetAxis("Mouse Y") > 0)
			{
				m_SpriteRenderer.sprite = birdDown;
			}
			
			if (Input.GetAxis("Mouse Y") <= 0)
			{
				m_SpriteRenderer.sprite = birdUp;
			}
			*/
			
		//Change Sprites
			if (Input.GetAxis("Mouse Y") > 0)
			{
				m_Animator.SetBool("isGoingDown", true);
				m_Animator.SetBool("isGoingUp", false);
			}
			
			if (Input.GetAxis("Mouse Y") <= 0)
			{
				m_Animator.SetBool("isGoingDown", false);
				m_Animator.SetBool("isGoingUp", true);
			}
		} 
		else if (movingDir > 0)
		{
			m_Animator.SetBool("isGoingDown", true);
			m_Animator.SetBool("isGoingUp", false);
		}
		else
		{
			m_Animator.SetBool("isGoingDown", false);
			m_Animator.SetBool("isGoingUp", true);
		}

		preYPos = currentPos;
	}

	void PlayerMovement()
	{		
		//Move Player Up OR Down
		if (Services.FlightSpeed.PlayerFlightSpeed() > 10)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(-11, 7.5f, 0), Time.deltaTime);
		}
		
		if ( Services.FlightSpeed.PlayerFlightSpeed() < 9)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(-11, -7.5f, 0), Time.deltaTime*0.5f);
		}
	}

	void PlayerRotation()
	{
		float rotationSpeed = 1/ (2 * 1 / Services.FlightSpeed.PlayerFlightSpeed());
		
		currentPos = transform.position.y;
		if (currentPos >= previousPos)
		{
			if (transform.position.y <= 4.5f)
			{
				degree = 35f;
				angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime * rotationSpeed);
			}
			else
			{
				degree = 0f;
				angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime * rotationSpeed);
			}
		} 
		else
		{
			if (transform.position.y >= -4.5f)
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
		}

		previousPos = currentPos;
	}

	void MoveableArea()
	{
		Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
 
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),Mathf.Clamp(transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 1), transform.position.z);
	}
	
	
	void PlayerRotation2()
	{
		currentPos = transform.position.y;
		
		if (Services.FlightSpeed.PlayerFlightSpeed() > 7)
		{	
			if (transform.position.y <= 2)
			{
				degree = 35f;
				angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
			}
			else
			{
				degree = 0f;
				angle = Mathf.LerpAngle(transform.rotation.z, degree, Time.deltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, degree), Time.deltaTime);
			}
		}
		if (Services.FlightSpeed.PlayerFlightSpeed() < 7)
		{			
			if (transform.position.y >= -2)
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
		}
	}
	
}
