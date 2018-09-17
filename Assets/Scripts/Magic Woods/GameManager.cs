using System.Collections;
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
		m_TreeManager = GameObject.Find("Tree Manager").GetComponent<TreeManager>();
		m_FlightSpeed = GameObject.Find("Flight Speed").GetComponent<FlightSpeed>();
	}
	

	void Update () {

		if (numOfMissedBirds > 6)
		{
			Debug.Log("You lost");
		}

		if (score > 12)
		{
			Debug.Log("You win!");
		}

	}
	
	private void FlightSpeed()
	{
		flightSpeed = m_FlightSpeed.flightSpeed;
	}
}
