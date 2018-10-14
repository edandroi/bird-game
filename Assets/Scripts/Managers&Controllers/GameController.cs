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
		
		Services.TreeManager = GameObject.Find("Tree Manager").GetComponent<TreeManager>();
		Services.PlayerBird = GameObject.Find("Player").GetComponent<PlayerBird>();
		Services.FlightSpeed = GameObject.Find("Flight Speed").GetComponent<FlightSpeed>();
	}
}
