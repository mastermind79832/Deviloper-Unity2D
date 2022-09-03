using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;

namespace Deviloper.Ability.Aura
{
    public class Aura : AbilityController
    {
        [SerializeField] private float m_Damage;

		protected override void Start()
		{
			base.Start();
		}
		private void OnTriggerStay2D(Collider2D other)
		{
			if(other.TryGetComponent(out IDamageable enemy))
			{
				enemy.TakeDamage(m_Damage/60);
			}
		}
	}
}