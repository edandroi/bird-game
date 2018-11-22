using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour
{
	private GameObject frontPar;
	private GameObject backPar;
	
	// Use this for initialization
	void Awake ()
	{
		frontPar = transform.GetChild(0).gameObject;
		backPar = transform.GetChild(1).gameObject;
		
		frontPar.SetActive(false);
		backPar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Services.Player.glidingNow())
		{
			frontPar.SetActive(true);
			backPar.SetActive(true);
		}
		else
		{
			frontPar.SetActive(false);
			backPar.SetActive(false);
		}
	}
}
