using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		_InitializeServices();
	}
	

	private void _InitializeServices()
	{
		Services.GameController = this;	
		
		// Color Manager
		var colorManagerGameObject = new GameObject("Color Manager");
		Services.ColorManager = colorManagerGameObject.AddComponent<ColorManager>();
		
		// Audio Manager
		var audioManagerGameObject = new GameObject("Audio Manager");
		Services.AudioManager = audioManagerGameObject.AddComponent<AudioManager>();
		
		// Animation Controller
		var feedbackAnimationsGameObject = new GameObject("Feedback Animations");
		Services.FeedbackAnimations = feedbackAnimationsGameObject.AddComponent<FeedbackAnimations>();
		
		// Bird Manager
		var birdManagerGameObject = new GameObject("Bird Manager");
		Services.BirdManager = birdManagerGameObject.AddComponent<BirdManager>();

		Services.Player = GameObject.Find("Player").GetComponent<Player>();
		Services.BackgroundManager = GameObject.Find("Game Manager").GetComponent<BackgroundManager>();
		
//		Services.TreeManager = GameObject.Find("Tree Manager").GetComponent<TreeManager>();
//		Services.PlayerBird = GameObject.Find("Player").GetComponent<PlayerBird>();
				
//		// Background Manager
//		var bgManagerGameObject = new GameObject("Background Manager");
//		Services.BackgroundManager = bgManagerGameObject.AddComponent<BackgroundManager>();
		
//		Services.FlightSpeed = GameObject.Find("Flight Speed").GetComponent<FlightSpeed>();
	}
}
