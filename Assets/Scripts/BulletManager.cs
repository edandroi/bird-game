using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class BulletManager : MonoBehaviour
{
	public GameObject bullet;
	public float bulletSpeed;

	public float timerMax;
	public float timerMin;
	private float timer;

	private Vector3 spawnPos;

	private FlightSpeed m_FlightSpeed;
	
	// Use this for initialization
	void Start ()
	{
		timer = 5f;
		m_FlightSpeed = GameObject.Find("FlightSpeed").GetComponent<FlightSpeed>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			if (m_FlightSpeed.flightSpeed < 15)
			{
				float newYPos = Random.Range(-4.5f, 4.5f);
				spawnPos = new Vector3(-15f, newYPos, 0);
				GameObject newBullet = Instantiate(bullet,spawnPos, Quaternion.identity);
				newBullet.transform.parent = gameObject.transform;
				timer = Random.Range(1f, timerMax - 2f);
			}
	

		}
	}
}
