using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MigrationBehavior : MonoBehaviour
{
	private float speed;
	
	void Start () {
		
	}
	
	void Update ()
	{
		speed = Random.Range(0.01f, 0.02f);
		transform.position += Vector3.right * speed;
	}
}
