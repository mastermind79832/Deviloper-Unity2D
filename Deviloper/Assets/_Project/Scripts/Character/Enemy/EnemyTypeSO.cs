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

		[System.Serializable]
		public struct PickupDropProperty
		{
			public PickupType pickupType;
			[Range(0, 100)]
			[Tooltip("percentage probability of dropping pickup")]
			public float dropRate;
		}

		public List<PickupDropProperty> dropProperties;

	}
}