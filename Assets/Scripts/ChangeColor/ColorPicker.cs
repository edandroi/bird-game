using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour {

	private SpriteRenderer m_SpriteRenderer;
	private Color m_NewColor;

	public KeyCode red;
	public KeyCode blue;
	public KeyCode yellow;

	public Color currentColor;
	
	// Use this for initialization
	void Start () {
		currentColor = Color.clear;
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(red))
		{
			m_SpriteRenderer.color = Color.red;
		
		}

		if (Input.GetKeyDown(blue))
		{
			m_SpriteRenderer.color = Color.blue;
		}

		if (Input.GetKeyDown(yellow))
		{
			m_SpriteRenderer.color = Color.yellow;
		}

		currentColor = m_SpriteRenderer.color;

	}
}
