using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	private Mesh m_Mesh;
	// if we already spawned one following bg, don't spawn a new one.
	private bool spawned;
	
	void Start ()
	{
		m_Mesh = GetComponent<MeshFilter>().mesh;
		spawned = false;
//		Debug.Log("width: "+m_SpriteRenderer.bounds.max.x);
//		Debug.Log("height: "+m_SpriteRenderer.bounds.max.y);
	}
	
	void Update () 
	{
//		Debug.Log(m_Mesh.bounds.max);
		if (Camera.main.WorldToViewportPoint(m_Mesh.bounds.max).x <= 1 
		    && Camera.main.WorldToViewportPoint(m_Mesh.bounds.max).x > 0.90 && spawned == false)
		{
			float spawnXPos = m_Mesh.bounds.max.x + m_Mesh.bounds.extents.x;
			float spawnYPos = m_Mesh.bounds.max.y - m_Mesh.bounds.extents.y;
			spawned = true;
			Services.BackgroundManager.AddBackground(spawnXPos, spawnYPos);
		}

		if (Camera.main.WorldToViewportPoint(m_Mesh.bounds.max).x < 0)
		{
			Services.BackgroundManager.RemoveBackground(gameObject);
		}
	}
}
