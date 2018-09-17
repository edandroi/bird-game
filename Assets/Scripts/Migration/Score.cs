using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

	public int score;
	PlayerInput player;
	
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("bird").GetComponent<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		score = player.score;
		Debug.Log("score is "+score);
	}
}
