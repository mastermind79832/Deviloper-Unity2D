using System.Collections.Generic;
using UnityEngine;
using Deviloper.Character;

namespace Deviloper.Stage
{
	[CreateAssetMenu(menuName = "Stage/New Property", fileName = "New StageProperty")]
	public class StagePropertiesSO : ScriptableObject
	{
		public int level;
		public int enemyCount;
		public List<EnemyType> enemyList;
		public float enemySpawnInterval;
		public float nextStageTimeOut;
	}
}