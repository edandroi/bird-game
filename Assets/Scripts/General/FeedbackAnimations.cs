using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackAnimations : MonoBehaviour
{
	private GameObject voiceLinesAni;
	private GameObject birdReactionAni;

	void Awake()
	{
		voiceLinesAni = Resources.Load<GameObject>("Animations/Prefabs/LineAni");
		birdReactionAni = Resources.Load<GameObject>("Animations/Prefabs/BirdReactAni");
	}

	public void InstantiateVoiceLines(Vector3 initialPos, Quaternion objRotation)
	{
		Vector2 voiceDir = initialPos + transform.right * 2;
		Instantiate(voiceLinesAni, voiceDir, objRotation, gameObject.transform);
	}

	public void BirdReactionAni(GameObject birdObj)
	{
		Instantiate(birdReactionAni,birdObj.transform.position, birdObj.transform.rotation, gameObject.transform);
		
	}
}
