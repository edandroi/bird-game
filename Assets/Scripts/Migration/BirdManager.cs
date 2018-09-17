using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{

	private List<GameObject> allBirds;


	private void Awake()
	{
		allBirds = new List<GameObject>();
		foreach (GameObject birdObj in GameObject.FindGameObjectsWithTag("bird"))
		{
			allBirds.Add(birdObj);
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RemoveBirds(GameObject toDestroy)
	{
		allBirds.Remove(toDestroy);
		Destroy(toDestroy);
	}

	public int NumberOfBirds()
	{
		return allBirds.Count;
	}
}
