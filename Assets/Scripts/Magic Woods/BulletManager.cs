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
	
	// Use this for initialization
	void Start ()
	{
		timer = 5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			float newYPos = Random.Range(-4.5f, 4.5f);
			spawnPos = new Vector3(-15f, newYPos, 0);
			GameObject newBullet = Instantiate(bullet,spawnPos, Quaternion.identity);
			newBullet.transform.parent = gameObject.transform;

			timer = Random.Range(timerMin, timerMax);
		}
	}
}
