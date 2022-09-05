using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;
using Deviloper.Stage;
using Deviloper.Character;
using Deviloper.Service.Character;
using Deviloper.UI;
using TMPro;

namespace Deviloper.Service.Stage
{
    public class StageService : MonoSingletonGeneric<StageService> 
    {

		[Header("Stage Properties")]
		[Tooltip("The stage property will be used unity next stage level is encountered. element 0 is bydefault for level 1")]
        public List<StagePropertiesSO> stages;

		[SerializeField] private StageController m_CurrentStage;
		private int m_CurrentStageLevel;
		private int m_CurrentStageIndex;

		private CharacterService characterService;

		[SerializeField] private TextMeshProUGUI m_StageText;

		private void Start()
		{
			characterService = CharacterService.Instance;
			InitializeFirstStage();
		}

		private void InitializeFirstStage()
		{
			m_CurrentStage = new StageController();
			m_CurrentStage.SetStageService();
			m_CurrentStageIndex = 0;
			m_CurrentStageLevel = 1;
			m_CurrentStage.SetNewStage(m_CurrentStageLevel, stages[m_CurrentStageIndex]);
			UpdateStageNumberText();
		}

		private void Update()
		{
			if(!UiController.Instance.isGamePlaying)
				return;

			if (characterService.IsEnemyListEmpty() && m_CurrentStage.IsEnemyOver())
			{
				m_CurrentStage.WaitForNextStage();
				return;
			}

			m_CurrentStage.Update();
		}

		public void SpawnEnemy(EnemyType enemyPrefab)
		{
            characterService.SpawnEnemy(enemyPrefab,m_CurrentStage.level);
		}

		public void StartNextStage()
		{
			m_CurrentStageLevel++;

			if (m_CurrentStageIndex + 1 < stages.Count)
			{
				if (m_CurrentStageLevel == stages[m_CurrentStageIndex + 1].level)
				{
					m_CurrentStageIndex++;
				}
			}

			m_CurrentStage.SetNewStage(m_CurrentStageLevel, stages[m_CurrentStageIndex]);
			UpdateStageNumberText();
		}

		private void UpdateStageNumberText()
		{
			m_StageText.text = $"STAGE : {m_CurrentStageLevel}";
		}
	}
}
