using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerDirection : MonoBehaviour
{
	private LineRenderer m_LineRenderer;
	private Vector2 playerPosition;
	private Vector2 raycastDir;
	
	void Start ()
	{
		m_LineRenderer = GetComponent<LineRenderer>();
		m_LineRenderer.enabled = false;
	}
	
	void Update ()
	{
		transform.rotation = Services.PlayerBird.transform.rotation;
		Vector2 direction = transform.right*30;
		playerPosition = Services.PlayerBird.transform.position + new Vector3(0, -0.1f, 0);
		raycastDir = playerPosition + direction;

		m_LineRenderer.SetPosition(0, playerPosition);
		m_LineRenderer.SetPosition(1, raycastDir);

		if (Input.GetMouseButton(1))
		{
			m_LineRenderer.enabled = true;
		}
		else
		{
			m_LineRenderer.enabled = false;
		}
	}
}
