using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableArea : MonoBehaviour
{
	bool down = false;
	bool diving = false;
	
	void Update () 
	{
		if (transform.position.y >= Services.Player.UpperLimit())
		{
			diving = true;
		}
		
		if (transform.position.y <= Services.Player.BottomLimit())
		{
			Debug.Log("bottom limit reached");
			gameObject.GetComponent<Player>().enabled = false;
			down = true;
		}

		while (diving == true || down == true)
		{
			Debug.Log("I work");
			gameObject.GetComponent<Player>().enabled = false;
			LimitsReached();
		}

	}
	
	void LimitsReached()
	{

		if (down)
		{
			StartCoroutine(DownLimitReached(transform.position.x + 10, transform.position.y + 20, transform.position.z, 0.2f));
			gameObject.GetComponent<Player>().enabled = true;
			down = false;
		}
		
		if (diving)
		{
			StartCoroutine(UpperLimitReached(transform.position.x - 10, transform.position.y - 20, transform.position.z, 0.2f));
			gameObject.GetComponent<Player>().enabled = true;
			diving = false;
		}
	}
	
	IEnumerator DownLimitReached(float x, float y, float z, float t)
	{
		transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, 20f, Time.deltaTime));
		transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), t );
		yield return null;
	}

	IEnumerator UpperLimitReached(float x, float y, float z, float t)
	{
		transform.eulerAngles = new Vector3(0,0, Mathf.LerpAngle(transform.eulerAngles.z, -20f, Time.deltaTime));
		transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), t );
		yield return null;
	}
}
