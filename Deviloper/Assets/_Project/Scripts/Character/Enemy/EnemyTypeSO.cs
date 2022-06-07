using System.Collections;
using UnityEngine;

namespace Deviloper.Character
{
	[CreateAssetMenu(menuName = "Character/New EnemyType",fileName ="New EnemyType")]
	public class EnemyTypeSO : ScriptableObject
	{
		public float baseHealth;
		public float baseSpeed;
		public float baseDamage;
	}
}