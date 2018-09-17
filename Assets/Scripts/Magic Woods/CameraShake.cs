using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// © 2017 TheFlyingKeyboard and released under MIT License
// theflyingkeyboard.net

public class CameraShake : MonoBehaviour {
	public Vector3 axisShakeMin;
	public Vector3 axisShakeMax;
	public float timeOfShake;
	private float timeOfShakeStore;
	private bool shake;
	private Vector3 startPos;
	
	// Use this for initialization
	void Start () {
		
		shake = false;
		startPos = transform.position;
		timeOfShakeStore = timeOfShake;
	}
  
	// Update is called once per frame
	void Update ()
	{

		if (shake == true)
		{
			while (timeOfShake >= 0)
			{
				transform.position = startPos+new Vector3(Random.Range(axisShakeMin.x, axisShakeMax.x),
					Random.Range(axisShakeMin.y, axisShakeMax.y), Random.Range(axisShakeMin.z, axisShakeMax.z));
				timeOfShake -= Time.deltaTime;
			}

			if (timeOfShake < 0)
			{
				shake = false;
				transform.position = Vector3.Lerp(transform.position, startPos, 2f);
			}
		}
	}
	public void ShakeCamera(float shakeTime = -1.0f)
	{
//		if (shakeTime > 0.0f)
//		{
//			timeOfShake = shakeTime;
//		}
//		else
//		{
//			timeOfShake = timeOfShakeStore;
//		}
		timeOfShake = timeOfShakeStore;
		shake = true;
		
	}
}