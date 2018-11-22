using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{

	float speed;
	private bool seen;
	
	void Start ()
	{
		speed = Random.Range(GameObject.Find("CloudManager").GetComponent<CloudManager>().minSpeed, 
			GameObject.Find("CloudManager").GetComponent<CloudManager>().maxSpeed);
	}
	
	void Update ()
	{
//		transform.position += Vector3.right * speed;
	}

	/*
	void OnTriggerEnter2D(Collider2D other)
	{	
		if (other.gameObject.CompareTag("killzone"))
		{
			Destroy(gameObject);
		}
	}
	*/
	
	void OnBecameInvisible() 
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("cloud"))
			{
				Destroy(gameObject);
			}
	}
}
