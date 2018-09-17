using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_GameManager : MonoBehaviour
{
	private Camera mainCamera;
	private Color bgColor;

	private BirdManager b_Manager;
	private int birdCount;

	public int score;
	
	
	// Use this for initialization
	void Start ()
	{
		mainCamera = Camera.main.GetComponent<Camera>();
		
		b_Manager = GameObject.Find("Bird Manager").GetComponent<BirdManager>();
		
//		lines = GameObject.Find("Goal Manager").GetComponent<>()
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		birdCount = b_Manager.NumberOfBirds();
	
		switch (birdCount)
		{
				case 6:
					mainCamera.backgroundColor = new Color32(93, 3, 226, 255);
					break;
				case 5:
					mainCamera.backgroundColor = new Color32(130, 3, 226, 255);
					break;
				case 4:
					mainCamera.backgroundColor = new Color32(130, 3, 226, 255);
					break;
				case 3:
					mainCamera.backgroundColor = new Color32(219, 66, 133, 255);
					break;
				case 2:
					mainCamera.backgroundColor = new Color32(227, 101, 4, 255);
					break;
				case 1:
					mainCamera.backgroundColor = new Color32(207, 95, 64, 255);
					break;
				case 0:
					mainCamera.backgroundColor = new Color32(219, 72, 66, 255);
					break;
		}
		
		//bg color 67 / 37/ 142  43258E
		
	}


//	public void TrackScore(int i)
//	{
//	}
}
