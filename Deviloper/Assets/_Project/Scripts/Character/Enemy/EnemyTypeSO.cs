using System.Collections.Generic;
using UnityEngine;
using Deviloper.Pickup;

namespace Deviloper.Character
{
	public enum EnemyType
	{
		Normal = 0,
		Fast = 1,
		Heavy = 2
	}

	[CreateAssetMenu(menuName = "Character/New EnemyType",fileName ="New EnemyType")]
	public class EnemyTypeSO : ScriptableObject
	{
		public float baseHealth;
		public float baseSpeed;
		public float baseDamage;


	}
}