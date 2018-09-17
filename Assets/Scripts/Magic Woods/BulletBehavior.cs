using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
	private float speed;

	// Use this for initialization
	void Start ()
	{
		speed = GameObject.Find("Bullet Manager").GetComponent<BulletManager>().bulletSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += transform.right * speed * Time.deltaTime;
	}
}
