using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStopTest : MonoBehaviour {

	public ParticleSystem particles;

	// Update is called once per frame
	void Update () {
		//After 4 seconds, pause particles
		if(Time.timeSinceLevelLoad > 4f) {
			particles.Pause();
		}
	}
}
