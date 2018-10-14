using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBird : MonoBehaviour
{
	
	// Sprites
	public Sprite birdUp;
	public Sprite birdDown;
	
	//Keys for changing colors
	public KeyCode c_Btn1;
	public KeyCode c_Btn2;
	public KeyCode c_Btn3;
	
	public Color32 pickedColor;
	
	private SpriteRenderer m_SpriteRenderer;
	private Color m_NewColor;
	
	// player health
	public float playerHealth;

	private float flightSpeedNow;
	private float flightSpeedPre;
	
	private float degree;
	private float angle;

	void Start () 
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		pickedColor = m_SpriteRenderer.color;
	}
	

	void Update ()
	{
		MoveableArea(); // The screen space that the player is allowed to move
//		ChangePlayerColor(); // Controls how players can change color
		PlayerMovement(); // Controls how the player moves
		PlayerRotation(); // Controls player rotation
	}

	
	// FUNCTIONS
	void ChangePlayerColor()
	{
		m_SpriteRenderer.color = pickedColor;
		
		// Pick Color
		if (Input.GetKeyDown(c_Btn1))
		{
			pickedColor = Services.ColorManager.C_pink;
		}

		if (Input.GetKeyDown(c_Btn2))
		{
			pickedColor = Services.ColorManager.C_blue;
		}

		if (Input.GetKeyDown(c_Btn3))
		{
			pickedColor = Services.ColorManager.C_yellow;
		}
	}
	
	void PlayerMovement()
	{
		if(Input.GetMouseButton(0)){
			//Change Sprites
			if (Input.GetAxis("Mouse Y") > 0)
			{
				m_SpriteRenderer.sprite = birdDown;
			}
			
			if (Input.GetAxis("Mouse Y") <= 0)
			{
				m_SpriteRenderer.sprite = birdUp;
			}
		}
		
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
		if (Services.FlightSpeed.PlayerFlightSpeed() > 11)
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
		if (Services.FlightSpeed.PlayerFlightSpeed() < 10)
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

	void MoveableArea()
	{
		Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
 
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),Mathf.Clamp(transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 1), transform.position.z);
	}
	
}
