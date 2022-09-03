using System.Collections;
using UnityEngine;
using Deviloper.Character;
using Deviloper.Core;

namespace Deviloper.Ability.ElectricFence
{
	public class ElectricFence : AbilityController
	{
		[SerializeField] private float m_Damage;
		[SerializeField] private float m_SlowDownMultiplier;

		protected override void Start()
		{
			base.Start();
		}
		
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.TryGetComponent(out EnemyController enemy))
			{
				enemy.TakeDamage(m_Damage);
				enemy.SlowDown(m_SlowDownMultiplier);
			}
		}
	}
}