﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Imphenzia;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public GameObject background;
	public GameObject m_Tip1;
	public GameObject m_Tip2;
	
	private List<GameObject> allBackgrounds;
	void Awake ()
	{
		allBackgrounds = new List<GameObject>();
	}

	private void Start()
	{
		AddBackground(Camera.main.transform.position.x, Camera.main.transform.position.y);
		
	}

	public void AddBackground(float xCoordinate, float yCoordinate)
	{

		Vector3 spawnPos = 
			new Vector3(xCoordinate, yCoordinate, 300);
		GameObject newBackground = Instantiate(background,
			spawnPos, Camera.main.transform.rotation);
		allBackgrounds.Add(newBackground);
	}

	public void RemoveBackground(GameObject backgroundObj)
	{
		allBackgrounds.Remove(backgroundObj);
		Destroy(backgroundObj);
	}

	public void AddBottomObject()
	{

	}
}
