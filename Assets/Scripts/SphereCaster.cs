using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour
{
	public GameObject currentHitObj;

	public float sphereRadius;
	public float maxDistance;
	public float distance;
	public float minDepth;
	public LayerMask m_LayerMask;

	private Vector2 origin;
	private Vector2 direction;
	
	void Start () {
		
	}
	
	void Update ()
	{
		origin = transform.position;
		direction = transform.forward;

		RaycastHit2D hit;
		if (Physics2D.CircleCast(origin, sphereRadius, direction, distance, m_LayerMask, minDepth, maxDistance 
			))
		{
			//currentHitObj = hit.transform.gameObject;
			Debug.Log("I hitted!");
			
		}
	}
}
