using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Character
{
    public class CharacterFactory : MonoBehaviour
    {
		[Header("Player")]
        public PlayerController playerPrefab;

        [System.Serializable]
        public struct EnemyTypePair
        {
            public EnemyType type;
            public EnemyController controller;
        }
        [Header("Enemy")]
        public List<EnemyTypePair> enemyPrefabs;
	
        public PlayerController CreatePlayer(Transform location)
		{
            return Instantiate(playerPrefab,location.position,location.rotation);
		}

        public EnemyController CreateEnemy(Vector2 location,EnemyType enemyType)// add type here
		{
            EnemyController enemyPreafb = GetEnemyTypePrefab(enemyType);
            return Instantiate(enemyPreafb, location, Quaternion.identity);
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
	}
}
   