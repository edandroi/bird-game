using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

 float speed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		speed = .02f;
		
		transform.position += Vector3.right * speed;
	}
}
