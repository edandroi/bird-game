using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	private AnimationClip playerVoiceAni;
	

	void Awake ()
	{
		playerVoiceAni = Resources.Load<AnimationClip>("Animations/playerVoiceAni");
	}
	
	void Update () {
		
	}

	public void PlayerVoiceAni(GameObject parentObj)
	{
		var playerVoiceObj = new GameObject("Player Voice");
		Animator m_Animator = playerVoiceObj.AddComponent<Animator>();
		
//		Animation m_Animation = playerVoiceObj.AddComponent<Animation>();
//		m_Animation.AddClip(playerVoiceAni, "playerVoice");
		m_Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/playerVoiceAni");
		m_Animator.Play("playerVoice");
		
		
		Vector2 voiceDir = parentObj.transform.position + parentObj.transform.right * 3;
		Instantiate(playerVoiceObj, voiceDir, Quaternion.identity, parentObj.transform);
	}
}
