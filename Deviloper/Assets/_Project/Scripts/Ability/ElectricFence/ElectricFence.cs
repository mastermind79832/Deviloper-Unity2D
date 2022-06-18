using System.Collections;
using UnityEngine;
using Deviloper.Character;
using Deviloper.Core;

namespace Deviloper.Ability.ElectricFence
{
	public class ElectricFence : MonoBehaviour
	{
		public float damage;
		public float slowDownMultiplier;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.TryGetComponent(out EnemyController enemy))
			{
				enemy.TakeDamage(damage);
				enemy.SlowDown(slowDownMultiplier);
			}
		}
	}
}