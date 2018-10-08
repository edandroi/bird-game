using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour {
	public float floatHeight;
	public float liftForce;
	public float damping;
	public Rigidbody2D rb2D;
	void Start() {
		
	}

	void FixedUpdate()
	{
	}

	void Update()
	{
		Vector3 forward = transform.TransformDirection(Vector3.right) * 10;
		Debug.DrawRay(transform.position, forward, Color.green);
	}
         
	
	//RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*30);
//		Debug.DrawLine(transform.position, Vector2.right*30, Color.green);
//		if (Input.GetKeyDown(KeyCode.E))
//		{
//			
//			if (hit.collider.CompareTag("bird"))
//			{
//				Debug.Log("Hi Bird!");
//			}	
//		}


}