using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBird : MonoBehaviour
{
	
	// Keys for movement
	public KeyCode right;
	public KeyCode left;
	
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

	private Vector3 initialPos;
	
	// player health
	public float playerHealth;
	
	// Use this for initialization
	void Start () 
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		pickedColor = m_SpriteRenderer.color;

		initialPos = transform.position;
	}
	

	void Update ()
	{
		PlayerInput();
		
		Debug.Log("player health is "+playerHealth);
	}

	
	
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("bullet"))
		{
			Debug.Log("Shot!");
			playerHealth -= 1f;
		}
	}

	void PlayerInput()
	{
		m_SpriteRenderer.color = pickedColor;

		//Change Sprites
		if (Input.GetAxis("Mouse Y") > 0)
		{
			m_SpriteRenderer.sprite = birdDown;
		}
		
		if (Input.GetAxis("Mouse Y") <= 0)
		{
			m_SpriteRenderer.sprite = birdUp;
		}

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

		if (Input.anyKey == false && transform.position.x != 0)
		{
			transform.position = Vector3.Lerp(transform.position, initialPos, Time.deltaTime);
		}
		
	}

}
