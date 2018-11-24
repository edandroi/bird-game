using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

	public GameObject cloud1;
	public GameObject cloud2;
	public GameObject cloud3;
	public GameObject cloudExplode;

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
		float newYPos = Services.Player.transform.position.y;
		float newXPos = Services.Player.transform.position.x + 50f;
		transform.position = new Vector3(newXPos, newYPos, transform.position.z);
		
		GenerateClouds();
	}

	void GenerateClouds()
	{
		
		timer -= Time.deltaTime;

		if (timer < 0)
		{
			Vector3 spawnPosition = Vector3.zero;
			Vector3 playerPosition = Services.Player.transform.position;
			
			if (Services.Player.flightDirection() > 0)
			{
				spawnPosition = new Vector3
					(Random.Range(playerPosition.x+20, playerPosition.x+50), Random.Range(playerPosition.y+40, playerPosition.y+60), Random.Range(playerPosition.z+10, playerPosition.z+90));
			}
			else
			{
				spawnPosition = new Vector3
					(playerPosition.x + 60, Random.Range(playerPosition.y+20, playerPosition.y+50), Random.Range(playerPosition.z+10, playerPosition.z+90));
			}
			
			Vector3 spawnPosExplode = new Vector3(spawnPosition.z, spawnPosition.y, Services.Player.transform.position.z);

//			playerPosition = Camera.main.ScreenToWorldPoint
//				(Services.Player.transform.position);
			
			switch (Random.Range(0, numOfClouds))
			{
				case 0:
					Instantiate(cloud1,spawnPosition,Quaternion.identity); 
					break;
				case 1: 
					Instantiate(cloud2,spawnPosition,Quaternion.identity);
					break;
				case 2:
					Instantiate(cloudExplode,spawnPosExplode,Quaternion.identity);
					break;	
			}
		
//			Instantiate(cloudExplode,spawnPosition,Quaternion.identity);
			timer = Random.Range(1f, 4f);
		}
	}
}
