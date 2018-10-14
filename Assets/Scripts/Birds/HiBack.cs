using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiBack : MonoBehaviour {

	void SayHiBack()
	{
		Services.FeedbackAnimations.InstantiateVoiceLines(transform.position, transform.rotation);
		Services.AudioManager.BirdSaysHi();
		Debug.Log("Hi Player Bird!");
	}
}
