using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Character;
using Deviloper.Service.Stage;

namespace Deviloper.stage
{
	[System.Serializable]
    public class StageController
    {
        public int level;
        public int enemyCount;
        public List<EnemyController> enemyList;
        public float enemySpawnInterval;
        public float nextStageTimeOut;

        private float spawnTimer = 0;
		private float nextStageTimer = 0;

		private StageService stageService;

		public void SetStageService() => stageService = StageService.Instance;

		public void SetNewStage(int _level, StageController stage)
		{
			level = _level;
			CopyStageProperties(stage);
			ResestAllTimer();
		}

		private void CopyStageProperties(StageController stage)
		{
			enemyCount = stage.enemyCount;
			enemyList = stage.enemyList;
			enemySpawnInterval = stage.enemySpawnInterval;
			nextStageTimeOut = stage.nextStageTimeOut;
		}

		private void ResestAllTimer()
		{
			spawnTimer = 0;
			nextStageTimer = 0;
		}

		public void Update()
		{
			RunStage();
		}

		public void WaitForNextStage()
		{
			IncreaseNextStageTimer();
			if(IsStageOver())
			{
				stageService.StartNextStage();
			}
		}

		private void IncreaseNextStageTimer() => nextStageTimer += Time.deltaTime;
		private void IncreaseEnemyTimer() => spawnTimer += Time.deltaTime;

		private void RunStage()
		{
			IncreaseEnemyTimer();
			if (spawnTimer >= enemySpawnInterval && enemyCount>0)
			{
				ResetEnemyTimer();
				stageService.SpawnEnemy(GetRandomEnemy());
				enemyCount--;
			}
		}
        public EnemyController GetRandomEnemy() => enemyList[Random.Range(0, enemyList.Count)];

		private void ResetEnemyTimer() => spawnTimer = 0;
		public bool IsEnemyOver() => enemyCount == 0;
		public bool IsStageOver() => nextStageTimer >= nextStageTimeOut;

	}
}
