using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class FollowOtherObjects : MonoBehaviour
{
	public float xInput;
	public float yInput;
	public GameObject followedObj;

	void Update () {
		FollowOverlap(xInput, yInput);
	}

	void FollowBehind()
	{
		transform.position = Vector3.Lerp(transform.position, followedObj.transform.position, 0.1f);
	}

	void FollowOverlap(float xValue, float yValue)
	{
		transform.position = new Vector3(followedObj.transform.position.x + xValue, followedObj.transform.position.y + yValue, 
			followedObj.transform.position.z) ;
	}
}
