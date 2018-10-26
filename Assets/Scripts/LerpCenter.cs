using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCenter : MonoBehaviour
{

	public GameObject lerpObj;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, lerpObj.transform.position, Time.deltaTime*15);
	}
}
