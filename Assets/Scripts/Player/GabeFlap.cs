using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

public class GabeFlap : MonoBehaviour
{
	public Vector2 velocity;

	public float[] gravity;
	public float drag;
	public int flapState;
	private float mouseChange;
	private float mousePosPre;
	public float flapThreshhold;
	public Vector2 flapForce;
	private Animator m_Animator;

	void Start () {
		//gravity=new float[3];
		m_Animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		velocity += Vector2.down*(gravity[flapState]);
		transform.position += (Vector3)velocity;
		transform.eulerAngles = new Vector3(0,0,Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));
	}

	void Update () 
	{
		float mouseY = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
		mouseChange += mouseY - mousePosPre;
		mousePosPre = mouseY;
		if (Mathf.Abs(mouseChange) > flapThreshhold)
		{
			if (mouseChange < 0 && flapState > 0) // if we're going down and if we weren't down already
			{
				velocity += flapForce;
			}
			flapState += (int)Mathf.Sign(mouseChange);
			flapState = Mathf.Clamp(flapState, 0, 2);
			
			mouseChange = 0;
		}

		switch (flapState)
		{
			case 0:
				m_Animator.SetBool("isGoingDown", true);
				m_Animator.SetBool("isGoingMiddle", false);
				m_Animator.SetBool("isGoingUp", false);
				break;
			case 1:
				m_Animator.SetBool("isGoingDown", false);
				m_Animator.SetBool("isGoingMiddle", true);
				m_Animator.SetBool("isGoingUp", false);
				break;
			case 2:
				m_Animator.SetBool("isGoingDown", false);
				m_Animator.SetBool("isGoingMiddle", false);
				m_Animator.SetBool("isGoingUp", true);
				break;
			
		}

	}
}
