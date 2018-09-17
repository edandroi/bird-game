using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{

	public GameObject cloud1;
	public GameObject cloud2;
	public GameObject cloud3;

	public float timer;

	public float minSpeed;
	public float maxSpeed;

	public int numOfClouds = 3;
	
	// Use this for initialization
	void Start ()
	{

		timer = 1f;
	}
	
	// Update is called once per frame
	void Update ()
	{

		timer -= Time.deltaTime;

		if (timer < 0)
		{
			Vector3 spawnPosition = Vector3.zero;
			float scaler = Random.RandomRange(0.7f, 1f);
			
			switch (Random.Range(0, numOfClouds))
			{
				case 0:
					spawnPosition = new Vector3(Random.Range(12f, 14f),Random.Range(-3.3f, 3.3f),0 );
					Instantiate(cloud1,spawnPosition,Quaternion.identity);
//					cloud0.transform.localScale = new Vector3(scaler, scaler, 1); 
					break;
				case 1: 
					spawnPosition = new Vector3(Random.Range(12f, 14f),Random.Range(-3.3f, 3.3f),0 );
					Instantiate(cloud1,spawnPosition,Quaternion.identity);
//					cloud1.transform.localScale = new Vector3(scaler, scaler, 1); 
					break;
				case 2:
					spawnPosition = new Vector3(Random.Range(12f, 14f),Random.Range(-3.3f, 3.3f),0 );
					Instantiate(cloud3, spawnPosition, Quaternion.identity);
					break;
					
			}
			timer = Random.Range(1f, 4f);
		}

	}
}
