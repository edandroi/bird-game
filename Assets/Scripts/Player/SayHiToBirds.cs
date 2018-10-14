using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayHiToBirds : MonoBehaviour
{

	private GameObject voiceLines;
	
	// Update is called once per frame
	void Update () 
	{
		Raycasting();
	}

	void Raycasting()
	{
		Vector2 startPos = transform.position;
		Vector2 direction = transform.right*30;
		Vector2 raycasted = startPos + direction;
		Debug.DrawLine(transform.position, raycasted, Color.green);
		//Physics.SphereCast(origin, thickness, direction, out hit)
		RaycastHit2D hit = Physics2D.Raycast(transform.position, raycasted);

		
		if (Input.GetMouseButtonDown(1))
		{
			//Visual and Audio Feedback
			Services.FeedbackAnimations.InstantiateVoiceLines(transform.position, transform.rotation);		
			Services.AudioManager.PlayerSaysHi();
			
			// Check if the bird heard our call
			if (hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "bird")
				{
					hit.collider.gameObject.SendMessage("SayHiBack");
				}
			}
		}
	}
}
