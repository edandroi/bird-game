using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private AudioSource m_AudioSource;
	private AudioSource bg_AudioSource;
	
	// Player
	private AudioClip playerHiSound;
	
	// Birds
	private AudioClip blueJay;
	
	//Environment
	private AudioClip windBgSound;

	private float throwSpeed = 2000f;
	private AudioSource source;
	private AudioSource sourceBg;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;
	private float waitForCall = 2;


	void Awake ()
	{
				
		// Player AudioClips
		playerHiSound = Resources.Load<AudioClip>("Sounds/Birds/rufousAntpitta");
		
		// Birds AudioClips
		blueJay = Resources.Load<AudioClip>("Sounds/Birds/blueJay1");
		
		//Environment AudioClips
		windBgSound = Resources.Load<AudioClip>("Sounds/Environment/wind-bg-1");
		
		m_AudioSource = gameObject.AddComponent<AudioSource>();
		source = GetComponent<AudioSource>();

		bg_AudioSource = gameObject.AddComponent<AudioSource>();
		sourceBg = GetComponent<AudioSource>();
		sourceBg.clip = windBgSound;
		sourceBg.volume = 0.3f;

	}

	private void Start()
	{
		BackgroundSound(.1f, .2f);
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

	void BackgroundSound(float min, float max)
	{
		float vol = Random.Range (min, max);
		sourceBg.Play();
		sourceBg.loop = true;
	}
}
