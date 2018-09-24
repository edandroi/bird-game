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
		Services.BulletManager = GameObject.Find("Bullet Manager").GetComponent<BulletManager>();
		Services.TreeManager = GameObject.Find("Tree Manager").GetComponent<TreeManager>();
		Services.ColorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
		Services.PlayerBird = GameObject.Find("Player").GetComponent<PlayerBird>();
		Services.FlightSpeed = GameObject.Find("Flight Speed").GetComponent<FlightSpeed>();
	}
}
