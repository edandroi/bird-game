using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
	// Keys for movement
	public KeyCode up;
	public KeyCode down;
	public KeyCode right;
	public KeyCode left;
	
	//Keys for changing colors
	public KeyCode c_Btn1;
	public KeyCode c_Btn2;
	
	public Color32 pickedColor;

	public float speed;
	public float angle;
	
	private SpriteRenderer m_SpriteRenderer;
	private Color m_NewColor;
	
	// Color Manager
	public ColorManager m_ColorManager;
	private Color32 C_pink;
	private Color32 C_blue;

	public CameraShake camShake;
	
	// Use this for initialization
	void Start () 
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		
		// Get the colors from Color Manager
		m_ColorManager = GameObject.Find("Color Manager").GetComponent<ColorManager>();
		C_pink = m_ColorManager.C_pink;
		C_blue = m_ColorManager.C_blue;

	

	}
	
	// Update is called once per frame
	void Update ()
	{
		m_SpriteRenderer.color = pickedColor;
		
		// Player Movement
		if (Input.GetKey(up))
		{
			transform.position += transform.up * speed * Time.deltaTime;
		}
		if (Input.GetKey(down))
		{
			transform.position -= transform.up * speed * Time.deltaTime;
		}

		// Player Rotation
		if (Input.GetKey(right))
		{
			transform.Rotate(transform.forward, angle * -1.0f);
		}
		if (Input.GetKey(left))
		{
			transform.Rotate(transform.forward, angle);
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
	}
}
