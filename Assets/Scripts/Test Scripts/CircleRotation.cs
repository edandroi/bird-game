using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotation : MonoBehaviour
{
	private Quaternion initialAngle;
	public float maxAngle;
	public float minAngle;
	private Quaternion maxRotate;
	private Quaternion minRotate;
	
	// Use this for initialization
	void Start ()
	{
		initialAngle = transform.rotation;
		maxRotate = new Quaternion(0, 0, Services.PlayerBird.transform.rotation.z+10, initialAngle.w);
		minRotate = new Quaternion(0, 0, Services.PlayerBird.transform.rotation.z-10, initialAngle.w);
	}
	
	// Update is called once per frame
	void Update () {
		maxRotate = new Quaternion(0, 0, Services.PlayerBird.transform.rotation.z+180, initialAngle.w);
		minRotate = new Quaternion(0, 0, Services.PlayerBird.transform.rotation.z-180, initialAngle.w);
//		Debug.Log(Services.PlayerBird.transform.rotation);
		if (Services.FlightSpeed.MovingUp() == true)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, maxRotate, Time.deltaTime);
		}
		else
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, minRotate, Time.deltaTime);
		}
	}
}
