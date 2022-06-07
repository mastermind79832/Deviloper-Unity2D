using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Character
{
    public class CharacterFactory : MonoBehaviour
    {
		[Header("Player")]
        public PlayerController playerPrefab;
        
        public List<EnemyController> enemyPrefabs;

        public PlayerController CreatePlayer(Transform location)
		{
            return Instantiate(playerPrefab,location.position,location.rotation);
		}

        public EnemyController CreateEnemy(Transform location)// add type here
		{
            return Instantiate(enemyPrefabs[0], location.position, location.rotation);
        }
    }
}
