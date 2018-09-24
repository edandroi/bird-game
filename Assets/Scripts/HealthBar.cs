using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	private Image timerImage;
	private PlayerBird m_PlayerBird;
	private float healthRemain;
	private float healthTotal;
	
	// Use this for initialization
	void Start () {
		timerImage = this.GetComponent<Image>();
		m_PlayerBird = GameObject.Find("Player").GetComponent<PlayerBird>();
		healthTotal = m_PlayerBird.playerHealth;
	}
	
	// Update is called once per frame
	void Update () {
		TimerAnimation();
	}

	void TimerAnimation()
	{
		healthRemain = m_PlayerBird.playerHealth;
		timerImage.fillAmount = healthRemain / healthTotal;
	}
}
