using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBird : MonoBehaviour
{
	public CameraShake camShake;
	
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

	public float speed;
	public float angle;
	
	private SpriteRenderer m_SpriteRenderer;
	private Color m_NewColor;
	
	// Color Manager
	public ColorManager m_ColorManager;
	private Color32 C_pink;
	private Color32 C_blue;
	private Color32 C_yellow;

	private Vector3 initialPos;
	
	// Use this for initialization
	void Start () 
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		pickedColor = m_SpriteRenderer.color;
	
		// Get the colors from Color Manager
		m_ColorManager = GameObject.Find("Color Manager").GetComponent<ColorManager>();
		C_pink = m_ColorManager.C_pink;
		C_blue = m_ColorManager.C_blue;
		C_yellow = m_ColorManager.C_yellow;

		initialPos = transform.position;
		
		camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
	}
	

	void Update ()
	{
		PlayerInput();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("bullet"))
		{
			camShake.ShakeCamera();
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
			pickedColor = C_pink;
		}

		if (Input.GetKeyDown(c_Btn2))
		{
			pickedColor = C_blue;
		}

		if (Input.GetKeyDown(c_Btn3))
		{
			pickedColor = C_yellow;
		}

		if (Input.anyKey == false && transform.position.x != 0)
		{
			transform.position = Vector3.Lerp(transform.position, initialPos, Time.deltaTime);
		}
		
	}

}
