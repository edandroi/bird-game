using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackAnimations : MonoBehaviour
{
	private GameObject voiceLinesAni;

	void Awake()
	{
		voiceLinesAni = Resources.Load<GameObject>("Animations/Prefabs/LineAni");
	}

	public void InstantiateVoiceLines(Vector3 initialPos, Quaternion objRotation)
	{
		Vector2 voiceDir = initialPos + transform.right * 2;
		Instantiate(voiceLinesAni, voiceDir, objRotation, gameObject.transform);
	}
}
