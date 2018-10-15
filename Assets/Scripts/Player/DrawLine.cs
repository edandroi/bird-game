using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
	private LineRenderer m_LineRenderer;
	private Vector2 playerPosition;
	private Vector2 raycastDir;
	
	void Start ()
	{
		m_LineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update ()
	{
		transform.rotation = Services.PlayerBird.transform.rotation;
		Vector2 direction = transform.right*30;
		playerPosition = Services.PlayerBird.transform.position + new Vector3(0, -0.1f, 0);
		raycastDir = playerPosition + direction;

		m_LineRenderer.SetPosition(0, playerPosition);
		m_LineRenderer.SetPosition(1, raycastDir);
	}
}
