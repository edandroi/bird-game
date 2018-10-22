using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiBack : MonoBehaviour
{
	private Animator m_Animator;
	private AnimatorClipInfo m_CurrentClipInfo;
	private float m_CurrentClipLenght;

	private void Awake()
	{
		m_Animator = GetComponent<Animator>();
	}

	void Update()
	{
		//Debug.Log(m_Animator.GetCurrentAnimatorStateInfo(0));
		
	}

	public void SayHiBack()
	{
		m_Animator.SetBool("isSayingHi", true);
		Services.FeedbackAnimations.BirdReactionAni(gameObject);
		Services.AudioManager.BirdSaysHi();
	}

	public void EndOfSinging()
	{
		m_Animator.SetBool("isSayingHi", false);
	}

}
