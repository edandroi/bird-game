﻿using System.Collections;
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
		
		var colorManagerGameObject = new GameObject("Color Manager");
		Services.ColorManager = colorManagerGameObject.AddComponent<ColorManager>();
		
//		var treeManagerGameObject = new GameObject("Tree Manager");
//		Services.TreeManager = treeManagerGameObject.AddComponent<TreeManager>();
		
		Services.TreeManager = GameObject.Find("Tree Manager").GetComponent<TreeManager>();
		Services.PlayerBird = GameObject.Find("Player").GetComponent<PlayerBird>();
		Services.FlightSpeed = GameObject.Find("Flight Speed").GetComponent<FlightSpeed>();
	}
}
