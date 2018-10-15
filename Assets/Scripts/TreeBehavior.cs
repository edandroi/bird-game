using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TreeBehavior : MonoBehaviour
{
	SpriteRenderer _playerSprite;
	private SpriteRenderer m_SpriteRenderer;
	private SpriteRenderer b_SpriteRenderer;
	private float speed;
	private Color32 initialColor;
	private FlightSpeed m_FlightSpeed;

	int matches;
	int missed;
	
	void Start () 
	{
		if (transform.position.y > 0)
		{
			transform.rotation = Quaternion.Euler(0,0,180);
		}
	}

	void Update ()
	{
		speed = Services.FlightSpeed.flightSpeed;
		
		if (transform.position.y < 0)
		{
			transform.position -= transform.right * speed * Time.deltaTime;
		}
		
		if (transform.position.y > 0)
		{
			transform.position += transform.right * speed * Time.deltaTime;
		}
	}

	
	
	private void OnTriggerEnter2D(Collider2D other)
	{		
		//KILLZONE
		if (other.gameObject.tag == "killzone")
		{
			Destroy(gameObject);
		}
	}	
	
}
