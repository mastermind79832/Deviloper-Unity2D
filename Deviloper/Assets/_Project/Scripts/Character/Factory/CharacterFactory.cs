using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Character
{
    public class CharacterFactory : MonoBehaviour
    {
		[Header("Player")]
        public PlayerController playerPrefab;

		[Header("Enemy")]
        public List<EnemyController> enemyPrefabs;

        public PlayerController CreatePlayer(Transform location)
		{
            return Instantiate(playerPrefab,location.position,location.rotation);
		}

        public EnemyController CreateEnemy(Vector2 location,EnemyController enemyPreafb)// add type here
		{
            return Instantiate(enemyPreafb, location, Quaternion.identity);
        }
    }
}
   