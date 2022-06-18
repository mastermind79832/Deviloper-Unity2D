using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;
using System;

namespace Deviloper
{
    public class UiController : MonoSingletonGeneric<UiController>
    {
        public GameObject startPanel;
        public GameObject endPanel;

		public bool isGameOver;
		public bool isGamePlaying;

		private void Start()
		{
			isGameOver = false;
			isGamePlaying = false;
			startPanel.SetActive(true);
			endPanel.SetActive(false);
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
			startPanel.SetActive(false);
		}

		public void GameOver()
		{
			isGamePlaying = false;
			isGameOver = true;
			endPanel.SetActive(true);
		}

		private void RestartGame()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}
