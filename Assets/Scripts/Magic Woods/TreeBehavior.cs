using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TreeBehavior : MonoBehaviour
{
	
	SpriteRenderer _playerSprite;
	private SpriteRenderer m_SpriteRenderer;
	private SpriteRenderer b_SpriteRenderer;
	private GameManager m_GameManager;
	private float speed;
	private Color32 initialColor;

	int matches;
	int missed;
	
	
	
	
	void Start () 
	{
		_playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		b_SpriteRenderer = transform.Find("parrot-w(Clone)").GetComponent<SpriteRenderer>();
		m_GameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		speed = GameObject.Find("Tree Manager").GetComponent<TreeManager>().speed;
		initialColor = m_SpriteRenderer.GetComponent<SpriteRenderer>().color;

		if (transform.position.y > 0)
		{
			transform.rotation = Quaternion.Euler(0,0,180);
		}
	}

	
	
	void Update ()
	{
		if (transform.position.y < 0)
		{
			transform.position -= transform.right * speed * Time.deltaTime;
		}
		
		if (transform.position.y > 0)
		{
			transform.position += transform.right * speed * Time.deltaTime;
		}

//		transform.position -= transform.right * speed * Time.deltaTime;
	}

	
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		//CHANGE COLOR TO PLAYER'S COLOR
		if (other.gameObject.CompareTag("Player"))
		{
			m_SpriteRenderer.color = _playerSprite.color;
		}
		
		
		//KILLZONE
		if (other.gameObject.tag == "killzone")
		{
			Destroy(gameObject);
		}

		
		//DETECT COLORS
		if (other.gameObject.CompareTag("color detector"))
		{
			if (transform.childCount != 0)
			{
				if (m_SpriteRenderer.color == b_SpriteRenderer.color)
				{
					m_GameManager.score += 1;
				}
				if ((m_SpriteRenderer.color != b_SpriteRenderer.color))
				{
					m_GameManager.numOfMissedBirds += 1;
				}
			}

			
			if (transform.childCount == 0)
			{
				if (m_SpriteRenderer.color != initialColor)
				{
					m_GameManager.numOfMissedBirds += 1;
				}
			}
			
		}
	}	
	
}
