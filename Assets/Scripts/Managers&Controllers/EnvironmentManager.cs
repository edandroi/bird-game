using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{

	public GameObject mountainTip1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InstantiateMountainTip(Vector3 position, Quaternion rotation)
	{
		Instantiate(mountainTip1, position, rotation);
	}
	
	
}
