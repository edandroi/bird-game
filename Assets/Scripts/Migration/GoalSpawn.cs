using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoalSpawn : MonoBehaviour
{

	public GameObject line;
	public float speed;
	public float timeToChangeDirection;

	private void Start()
	{
		timeToChangeDirection = Random.Range(1f, 6f);
	}

	// Update is called once per frame
	void Update ()
	{
		Instantiate(line, transform.position, Quaternion.identity);
		transform.position += transform.up * Time.deltaTime * speed;
		timeToChangeDirection -= Time.deltaTime;

		if (timeToChangeDirection < 0)
		{
			speed = speed * -1;
			timeToChangeDirection = Random.Range(1f, 6f);
		}

	}
}
