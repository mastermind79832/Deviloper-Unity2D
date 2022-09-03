using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;
using System;

namespace Deviloper.UI
{
    public class UiController : MonoSingletonGeneric<UiController>
    {
		[SerializeField] private GameObject m_StartPanel;
		[SerializeField] private GameObject m_EndPanel;
		public PlayerDetailUI PlayerDetailUI;
		public UpgradeUI UpgradeUI;

		public bool isGameOver;
		public bool isGamePlaying;

		private void Start()
		{
			isGameOver = false;
			isGamePlaying = false;
			m_StartPanel.SetActive(true);
			m_EndPanel.SetActive(false);
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if (isGameOver)
					RestartGame();
				else if (!isGamePlaying)
					StatGame();
			}
		}

		private void StatGame()
		{
			isGamePlaying = true;
			m_StartPanel.SetActive(false);
		}

		public void GameOver()
		{
			isGamePlaying = false;
			isGameOver = true;
			m_EndPanel.SetActive(true);
		}

		private void RestartGame()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}
