using System.Collections.Generic;
using UnityEngine;
using Deviloper.Character;
using Deviloper.Core;

namespace Deviloper.Service.Character
{
    public class CharacterService : MonoSingletonGeneric<CharacterService>
    {
		[Header("Factory")]
		public CharacterFactory factory;
		[Header("SpawnPoint")]
		public Transform playerSpawn;
		public float enemySpawnRadius;
		
        private PlayerController player;
        private List<EnemyController> enemies;

		private void Start()
		{
			enemies = new List<EnemyController>();
			player = factory.CreatePlayer(playerSpawn);
		}

		//player Stuff
		public Transform GetPlayerTransform()
		{
			return player.transform;
		}
	
		// Enemy stuff
		private Vector2 GetEnemySpawn()
		{
			Vector2 randomVector = new Vector2(
				GetRandomEnemyPoint(),
				GetRandomEnemyPoint());
			return randomVector;
		}

		private float GetRandomEnemyPoint()
		{
			int isNegative = Random.Range(-1, 2);
			if (isNegative == 0)
				isNegative = 1;
			return Random.Range(enemySpawnRadius * isNegative, (enemySpawnRadius + 11) * isNegative);
		}

		public void SpawnEnemy(EnemyType enemyPrefab, int stageLevel)
		{
			EnemyController newEnemy = factory.CreateEnemy(GetEnemySpawn(), enemyPrefab);
			newEnemy.SetStats(stageLevel);
			enemies.Add(newEnemy);
		}

		public bool IsEnemyListEmpty() => enemies.Count == 0;

		public void EnemyDeath(EnemyController enemy)
		{
			enemies.Remove(enemy);
			factory.BackToPool(enemy);
		}
		
	}
}
