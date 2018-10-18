using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Timeline;

public class TreeManager : MonoBehaviour
{
	public GameObject treeObj;

	public Transform spawnPos1;
	public Transform spawnPos2;
	private Transform spawnPos;
	
	private float timer;
	
	// Use this for initialization
	void Start ()
	{
		timer = 1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer -= Time.deltaTime;

		// Time to instantiate trees
		if (timer < 0)
		{
			switch (Random.Range(0, 2))
			{
				case 0:
					spawnPos = spawnPos1;
					break;
				
				case 1:
					spawnPos = spawnPos2;
					break;
					
			}
			// Instantiate tree
			var newTree = Instantiate(treeObj, spawnPos.position,Quaternion.identity) as GameObject;
			newTree.transform.parent = gameObject.transform;
			
			//Instantiate BIRDS			
			Services.BirdManager.InstantiateBirds(newTree);
			
			//RESET TIMER
			timer = Random.Range(1, 2);
		}		
	}
	
	public void DestroyTrees(GameObject tree)
	{
		Destroy(tree);
	}

}
