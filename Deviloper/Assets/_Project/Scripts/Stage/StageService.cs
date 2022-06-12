using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;
using Deviloper.stage;
using Deviloper.Character;
using Deviloper.Service.Character;
using System;

namespace Deviloper.Service.Stage
{
    public class StageService : MonoSingletonGeneric<StageService> 
    {

		[Header("Stage Properties")]
		[Tooltip("The stage property will be used unity next stage level is encountered. element 0 is bydefault for level 1")]
        public List<StagePropertiesSO> stages;

		[SerializeField]
        private StageController currentStage;
		private int currentStageLevel;
		private int currentStageIndex;

		private CharacterService characterService;

		private void Start()
		{
			characterService = CharacterService.Instance;
			InitializeFirstStage();
		}

		private void InitializeFirstStage()
		{
			currentStage = new StageController();
			currentStage.SetStageService();
			currentStageIndex = 0;
			currentStageLevel = 1;
			currentStage.SetNewStage(currentStageLevel, stages[currentStageIndex]);
		}

		private void Update()
		{
			if (characterService.IsEnemyListEmpty() && currentStage.IsEnemyOver())
			{
				currentStage.WaitForNextStage();
				return;
			}

			currentStage.Update();
		}

		public void SpawnEnemy(EnemyType enemyPrefab)
		{
            characterService.SpawnEnemy(enemyPrefab,currentStage.level);
		}

		public void StartNextStage()
		{
			currentStageLevel++;
			if (currentStageLevel == stages[currentStageIndex].level)
			{
				currentStageIndex++;
			}

			currentStage.SetNewStage(currentStageLevel, stages[currentStageIndex]);
		}
	}
}
