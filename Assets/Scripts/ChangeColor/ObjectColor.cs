using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour
{

	private SpriteRenderer m_SpriteRenderer;
	private Color m_NewColor;

	public Color currentColor;
	public GameObject player;

	// Use this for initialization
	void Start()
	{

		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		player = GameObject.Find("Player");

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player")
		{
			Debug.Log("Trigger");
			m_SpriteRenderer.color = player.GetComponent<Player>().pickedColor;
		}
	}
}
