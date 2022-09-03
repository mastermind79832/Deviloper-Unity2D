using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;
using System;

namespace Deviloper.UI
{
    public class UiController : MonoSingletonGeneric<UiController>
    {
		[SerializeField] private Animator m_TitleAnimator;
		[SerializeField] private GameObject m_EndPanel;
		private readonly int anim_IsTitleActive = Animator.StringToHash("IsActive");
		public PlayerDetailUI PlayerDetailUI;
		public UpgradeUI UpgradeUI;

		public bool isGameOver;
		public bool isGamePlaying;

		private void Start()
		{
			isGameOver = false;
			isGamePlaying = false;
			TitlePanelActive(true);
			m_EndPanel.SetActive(false);
			PlayerDetailUI.SetUIActive(false);
		}

		private void TitlePanelActive(bool isActive)
		{
			m_TitleAnimator.SetBool(anim_IsTitleActive, isActive);
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
			TitlePanelActive(false);
			PlayerDetailUI.SetUIActive(true);
		}

		public void GameOver()
		{
			isGamePlaying = false;
			isGameOver = true;
			m_EndPanel.SetActive(true);
			PlayerDetailUI.SetUIActive(false);
		}

		private void RestartGame()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}
