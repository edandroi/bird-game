using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCenter : MonoBehaviour
{
	

	public GameObject lerpObj;
	public float lerpSpeed;
	private Quaternion multiply;
	private Quaternion initialRot;
	
	public float playerAngle;
	public float currentRot;

	private float angle;

	public float multiplier;
	
	void Start () {
		multiply = new Quaternion(1, 1, 1.1f, 1);
		initialRot = transform.rotation;
	}
	
	void Update () {
	WeirdRotation();
		/*
		transform.position = Vector3.Lerp(transform.position, lerpObj.transform.position, Time.deltaTime*lerpSpeed);
		currentRot = transform.eulerAngles.z;
		playerAngle = Services.PlayerBird.transform.eulerAngles.z;
		angle = Mathf.LerpAngle(currentRot, playerAngle, Time.time);
		transform.eulerAngles = new Vector3(0, 0, angle);
		
		//Debug.Log("bird rotation is "+Services.PlayerBird.transform.eulerAngles);
		
		if (Services.FlightSpeed.MovingUp()== true)
		{
			angle = Mathf.LerpAngle(currentRot, playerAngle, Time.time);
			float addUp = 1;
			if (addUp < multiplier)
			{
				angle = Mathf.LerpAngle(currentRot, playerAngle+addUp, Time.time);
				transform.eulerAngles = new Vector3(0, 0, angle);
				addUp += 0.5f;
			}
			else
			{
				angle = Mathf.LerpAngle(currentRot, playerAngle+multiplier, Time.time);
				transform.eulerAngles = new Vector3(0, 0, angle);
			}
		}
	*/
		
	
	}

	
	
	void WeirdRotation()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, Services.PlayerBird.transform.rotation, Time.deltaTime*lerpSpeed);
//Debug.Log(Services.PlayerBird.transform.rotation.z);
		Debug.Log(transform.eulerAngles.z);
		
		if (Services.PlayerBird.transform.rotation.z > 0.1)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(initialRot.x, initialRot.y, Services.PlayerBird.transform.rotation.z+5, initialRot.w) * multiply, Time.deltaTime*lerpSpeed);
			Debug.Log(transform.rotation);
		}
	}
}
