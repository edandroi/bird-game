using System.Collections;
using System.Collections.Generic;
using Imphenzia;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour {

	// if we already spawned one following bg, don't spawn a new one.
	private bool spawned;
	private Mesh m_Mesh;
	private Vector3[] vertices;
	private Vector2[] uvs;
	private Bounds bounds;

	void Awake()
	{
		GameObject bgObj = new GameObject("background");
		bgObj.AddComponent<GradientSkyObject>();
	}

	void Start ()
	{
		m_Mesh = GetComponent<MeshFilter>().mesh;
		spawned = false;
	}

	void Update () 
	{
		LoopBackground();
	}

	void LoopBackground()
	{
		Debug.Log(m_Mesh.bounds.max.x );
		if (Camera.main.WorldToViewportPoint(m_Mesh.bounds.max).x <= 1 
		    && Camera.main.WorldToViewportPoint(m_Mesh.bounds.max).x > 0.99 && spawned == false)
		{
			Debug.Log("spawn new one");
			float spawnXPos = m_Mesh.bounds.max.x + m_Mesh.bounds.extents.x;
			float spawnYPos = m_Mesh.bounds.max.y - m_Mesh.bounds.extents.y;
			spawned = true;
			Services.BackgroundManager.AddBackground(spawnXPos, spawnYPos);
		}

		if (Camera.main.WorldToViewportPoint(m_Mesh.bounds.max).x < 0)
		{
			Debug.Log("Out of screen");
			Services.BackgroundManager.RemoveBackground(gameObject);
		}
	}
}
