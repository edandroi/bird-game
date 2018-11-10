using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

	public GameObject cloud1;
	public GameObject cloud2;
	public GameObject cloud3;
	public GameObject player;

	public float timer;

	public float minSpeed;
	public float maxSpeed;

	public int numOfClouds = 3;
	
	void Start ()
	{
		timer = 1f;
	}
	
	void Update ()
	{
		float newYPos = player.transform.position.y;
		float newXPos = player.transform.position.x + 50f;
		transform.position = new Vector3(newXPos, newYPos, transform.position.z);
		
		GenerateClouds();
	}

	void GenerateClouds()
	{
		
		timer -= Time.deltaTime;

		if (timer < 0)
		{
			Vector3 spawnPosition = Vector3.zero;
			
			switch (Random.Range(0, numOfClouds))
			{
				case 0:
					spawnPosition = new Vector3(transform.position.x,transform.position.y,0 );
					Instantiate(cloud1,spawnPosition,Quaternion.identity);
//					cloud0.transform.localScale = new Vector3(scaler, scaler, 1); 
					break;
				case 1: 
					spawnPosition = new Vector3(transform.position.x,transform.position.y,0 );
					Instantiate(cloud1,spawnPosition,Quaternion.identity);
//					cloud1.transform.localScale = new Vector3(scaler, scaler, 1); 
					break;
				case 2:
					spawnPosition = new Vector3(transform.position.x,transform.position.y,0 );
					Instantiate(cloud3, spawnPosition, Quaternion.identity);
					break;
					
			}
			timer = Random.Range(1f, 4f);
		}
	}
}
