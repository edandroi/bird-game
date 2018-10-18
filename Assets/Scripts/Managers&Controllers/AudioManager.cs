using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private AudioSource m_AudioSource;
	
	// Player
	private AudioClip playerHiSound;
	
	// Birds
	private AudioClip blueJay;


	private float throwSpeed = 2000f;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;


	void Awake ()
	{
		m_AudioSource = gameObject.AddComponent<AudioSource>();
		source = GetComponent<AudioSource>();
		
		// Player AudioClips
		playerHiSound = Resources.Load<AudioClip>("Sounds/Birds/rufousAntpitta");
		
		// Birds AudioClips
		blueJay = Resources.Load<AudioClip>("Sounds/Birds/blueJay1");
	}

	public void PlayerSaysHi()
	{
		float vol = Random.Range (volLowRange, volHighRange);
		source.PlayOneShot(playerHiSound,vol);
	}

	public void BirdSaysHi()
	{
		float vol = Random.Range (volLowRange, volHighRange);
		source.PlayOneShot(blueJay,vol);
	}
}
