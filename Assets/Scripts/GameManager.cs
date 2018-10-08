﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private TreeManager m_TreeManager;

	public int numOfMissedBirds;

	public int score;

	
	// Fly Speed
	private FlightSpeed m_FlightSpeed;
	private float flightSpeed;
	

	void Start ()
	{
		Cursor.visible = false;
	}
	
	void Update () {

		if (numOfMissedBirds > 10)
		{
			Debug.Log("You lost");
		}

		if (score > 12)
		{
			Debug.Log("You win!");
		}

	}
	
}
