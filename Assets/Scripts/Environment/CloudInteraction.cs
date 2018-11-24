using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudInteraction : MonoBehaviour
{
	private Animator m_Animator;
	private bool destroy = false;
	private float timer;
	public GameObject bird;
	
	void Start ()
	{
		m_Animator = GetComponent<Animator>();
		timer = 10f;
	}
	
	// Update is called once per frame
	void Update () {
//		m_Animator.SetBool("isExploded", true);
		
		if (destroy)
		{
			timer -= Time.deltaTime;
			if (timer < 0)
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			m_Animator.SetBool("isExploded", true);
			Instantiate(bird, transform.position, Quaternion.identity);
			destroy = true;
		}
	}
}
