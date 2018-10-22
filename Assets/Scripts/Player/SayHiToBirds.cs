using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayHiToBirds : MonoBehaviour
{

	private GameObject voiceLines;
	float circleRadius = 10f;
	public float distance = 40f;
	public LayerMask m_LayerMask;
	
	void Update () 
	{
		Raycasting();
	}

	void Raycasting()
	{
		Vector2 startPos = transform.position;
		Vector2 direction = transform.right * distance;
		Vector2 raycasted = startPos + direction;
		Debug.DrawLine(transform.position, raycasted, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("Birds"));
		if (Input.GetMouseButtonDown(1))
		{
			//Visual and Audio Feedback
			Services.FeedbackAnimations.InstantiateVoiceLines(transform.position, transform.rotation);		
			Services.AudioManager.PlayerSaysHi();

			if (Physics2D.CircleCast(startPos, circleRadius, direction, distance,m_LayerMask))
			{
				StartCoroutine(BirdsSayHiBack(hit));

			}
		}
	}
	
	IEnumerator BirdsSayHiBack(RaycastHit2D thisHit)
	{
		yield return new WaitForSeconds(0.4f);
		thisHit.collider.gameObject.GetComponent<HiBack>().SayHiBack();
	}
}
