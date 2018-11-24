using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	private SpriteRenderer m_SpriteRenderer;
	// if we already spawned one following bg, don't spawn a new one.
	private bool spawned;
	
	void Start ()
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		spawned = false;
//		Debug.Log("width: "+m_SpriteRenderer.bounds.max.x);
//		Debug.Log("height: "+m_SpriteRenderer.bounds.max.y);
	}
	
	void Update () 
	{
		if (Camera.main.WorldToViewportPoint(m_SpriteRenderer.bounds.max).x <= 1 
		    && Camera.main.WorldToViewportPoint(m_SpriteRenderer.bounds.max).x > 0.99 && spawned == false)
		{
			float spawnXPos = m_SpriteRenderer.bounds.max.x + m_SpriteRenderer.bounds.extents.x;
			float spawnYPos = m_SpriteRenderer.bounds.max.y - m_SpriteRenderer.bounds.extents.y;
			spawned = true;
			Services.BackgroundManager.AddBackground(spawnXPos, spawnYPos);
		}

		if (Camera.main.WorldToViewportPoint(m_SpriteRenderer.bounds.max).x < 0)
		{
			Services.BackgroundManager.RemoveBackground(gameObject);
		}
	}
}
