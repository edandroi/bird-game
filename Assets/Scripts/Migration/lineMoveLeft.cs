using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineMoveLeft : MonoBehaviour
{

	float speed = 3f;
	public int score = 0;
	
	// Update is called once per frame
	void Update ()
	{
		transform.position -= transform.right * Time.deltaTime * speed; 
	}

}
