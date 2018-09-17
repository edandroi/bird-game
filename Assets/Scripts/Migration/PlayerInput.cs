using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

	public KeyCode up;
	private Rigidbody2D rb;
	float force = 120f;
	private float currentForce;

	private SpriteRenderer playerSprite;
	private Sprite[] sprites;
	public Sprite birdUp;
	public Sprite birdDown;

	private GameObject birdManager;
	private BirdManager b_Manager;
	
	private SpriteRenderer m_SpriteRenderer;
	public Color m_NewColor;


	public int score;
	

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		playerSprite = GetComponent<SpriteRenderer>();

		birdManager = GameObject.Find("Bird Manager");
		b_Manager = birdManager.GetComponent<BirdManager>();
		
		currentForce = force;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetKey(up))
		{
			currentForce += 5;
			playerSprite.sprite = birdDown;
		}

		if (Input.GetKeyUp(up))
		{
			playerSprite.sprite = birdUp;
			rb.AddForce(transform.up*currentForce);
			currentForce = force;
		}

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Border")
		{
			rb.velocity = Vector2.zero;
		}
		
		if (other.gameObject.name == "Bird Killzone")
		{
			b_Manager.RemoveBirds(gameObject);
		}
		if (other.gameObject.tag == "score")
		{
			score++;
		}
	}

}
