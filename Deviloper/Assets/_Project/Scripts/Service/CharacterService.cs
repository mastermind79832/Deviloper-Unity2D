using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Character;
using Deviloper.Core;

namespace Deviloper.Service.Character
{
    public class CharacterService : MonoSingletonGeneric<CharacterService>
    {
		public CharacterFactory factory;
		public Transform playerSpawn;
		
        private PlayerController player;
        private List<EnemyController> enemies;

		private void Start()
		{
			player = factory.CreatePlayer(playerSpawn);
		}

		public void CreateEnemy(Transform location)
		{
			enemies.Add(factory.CreateEnemy(location));
		}
		
		public Transform GetPlayerTransform()
		{
			return player.transform;
		}
	}
}
