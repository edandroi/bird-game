using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsBackground : MonoBehaviour
{

	public float speed;
	

	void Update ()
	{
		transform.position -= Vector3.right * speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Cloud Killzone")
		{
			Destroy(gameObject);
		}
		
		if (other.gameObject.CompareTag("killzone"))
		{
			Destroy(gameObject);
		}
	}
}
