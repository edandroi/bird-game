using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

	float speed;
	
	// Use this for initialization
	void Start ()
	{

		speed = Random.Range(GameObject.Find("Cloud Generator").GetComponent<CloudGenerator>().minSpeed, 
			GameObject.Find("Cloud Generator").GetComponent<CloudGenerator>().maxSpeed);

	}
	
	// Update is called once per frame
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
