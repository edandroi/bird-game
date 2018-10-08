using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private AudioClip playerHiSound;
	private AudioSource m_AudioSource;


	private float throwSpeed = 2000f;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;


	void Awake ()
	{

		m_AudioSource = gameObject.AddComponent<AudioSource>();
		source = GetComponent<AudioSource>();
		playerHiSound = Resources.Load<AudioClip>("Sounds/Birds/violent-green-swallow-1");
		

	}


	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			//Debug.Log("I work!");
			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot(playerHiSound,vol);
		}
    
	}
}
