using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
	private ParticleSystem playerTrail;
	
	void Awake ()
	{
		playerTrail = GetComponent<ParticleSystem>();
		playerTrail.enableEmission = false;	
	}

	private void Start()
	{

	}

	void Update () {
		if (Services.Player.divingNow() || Services.Player.glidingNow())
		{
			StartCoroutine(runTrail(1f));
		}
		else
		{
			StartCoroutine(stopTrail(.5f));
		}

		if (Services.Player.divingNow() && Services.Player.flightDirection() > 0)
		{
			playerTrail.emissionRate = 0;
		}
	}
	
	IEnumerator runTrail(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		playerTrail.enableEmission = true;
		yield return null;
	}

	IEnumerator stopTrail(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		playerTrail.enableEmission = false;
		yield return null;
	}
}
