using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
	private GameObject birdObj;
	private GameObject birdObjSing;
	
	void Start () {
		birdObj = Resources.Load<GameObject>("Birds/blueJay");
	}
	
	public void InstantiateBirds(GameObject tree)
	{
		Vector3 birdSpawnPos = tree.transform.position + new Vector3(0, 1.5f, 0);
		var newBird = Instantiate(birdObj, birdSpawnPos, Quaternion.identity) as GameObject;
		newBird.transform.parent = tree.transform;
		
		int destroyTheBird = Random.Range(0, 2);
			
		switch (destroyTheBird) 
		{
			case 0:
				Destroy(newBird);
				break;
			case 1:
				break;
		}
	}

}
