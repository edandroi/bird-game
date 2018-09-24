using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLimit : MonoBehaviour
{

	// The maximum angle the board can tilt
	public float MaxTiltAngle;
	public float tiltSpeed; // tilting speed in degrees/second
	Vector3 curRot;
	float maxZ;
	float minZ;

	void Start()
	{
		// Get initial rotation
		curRot = this.transform.eulerAngles;
		// calculate limit angles:
		maxZ = curRot.z + MaxTiltAngle;
		minZ = curRot.z - MaxTiltAngle;
	}

	void Update()
	{
		// "rotate" the angles mathematically:
		curRot.z += Input.GetAxis("Horizontal") * Time.deltaTime * tiltSpeed;
		// Restrict rotation along x and z axes to the limit angles:
		curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);

		// Set the object rotation
		this.transform.eulerAngles = curRot;
	}
}
