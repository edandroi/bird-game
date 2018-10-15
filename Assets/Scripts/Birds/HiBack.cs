using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiBack : MonoBehaviour
{
	
	public void SayHiBack()
	{
		Services.FeedbackAnimations.BirdReactionAni(gameObject);
		Services.AudioManager.BirdSaysHi();
	}

}
