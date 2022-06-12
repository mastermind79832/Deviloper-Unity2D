using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;

namespace Deviloper.Character
{
    public class CharacterFactory : MonoBehaviour
    {
		[Header("Player")]
        public PlayerController playerPrefab;

        [System.Serializable]
        public class EnemyTypePair
        {
            public EnemyType type;
            public EnemyController controller;
            public ObjectPool<EnemyController> pool;
        }
        [Header("Enemy")]
        public Transform enemyCollection;
        public List<EnemyTypePair> enemyPrefabs;
	
        public PlayerController CreatePlayer(Transform location)
		{
            PlayerController newPlayer = Instantiate(playerPrefab, location.position, location.rotation);
            newPlayer.transform.SetParent(location);
            return newPlayer;
		}

        public EnemyController CreateEnemy(Vector2 location,EnemyType enemyType)
		{
            EnemyTypePair enemyTypePair = GetEnemytypePair(enemyType);
            EnemyController newEnemy;
            if (enemyTypePair.pool.IsEmpty())
			{
                newEnemy = Instantiate(GetEnemyTypePrefab(enemyType), location, Quaternion.identity);
                newEnemy.Type = enemyType;
                newEnemy.transform.SetParent(enemyCollection);
            }
            else
			{
                newEnemy = enemyTypePair.pool.GetItem();
                newEnemy.transform.position = location;
                newEnemy.gameObject.SetActive(true);
			}
            return newEnemy;
        }

		private EnemyTypePair GetEnemytypePair(EnemyType enemyType)
		{
            foreach (EnemyTypePair enemyPair in enemyPrefabs)
            {
                if (enemyPair.type == enemyType)
                    return enemyPair;
            }

            return enemyPrefabs[0];
        }

		private EnemyController GetEnemyTypePrefab(EnemyType enemyType)
		{
			foreach (EnemyTypePair enemyPair in enemyPrefabs)
			{
                if (enemyPair.type == enemyType)
                    return enemyPair.controller;
			}
            // If enemy type not found. Then spawn first one
            return enemyPrefabs[0].controller;
		}

        public void BackToPool(EnemyController enemy)
		{
            EnemyTypePair enemyTypePair = GetEnemytypePair(enemy.Type);
            enemyTypePair.pool.SetItem(enemy);
        }
	}
}
   