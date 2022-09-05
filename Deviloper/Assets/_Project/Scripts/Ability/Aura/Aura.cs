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
			m_AbilityName = "Aura";
			m_Detail_1 = "Damage";
			m_Detail_2 = "";
			m_Detail_3 = "";
			base.Start();
		}
		private void OnTriggerStay2D(Collider2D other)
		{
			if(other.TryGetComponent(out IDamageable enemy))
			{
				enemy.TakeDamage(m_Damage/60);
			}
		}
		protected override void UpdateUI()
		{
			m_UpgradeUI.RefreshUI(m_Level, m_Damage, 0, 0, m_UpgradeAmount);
		}

		protected override void Upgrade()
		{
			m_Damage += 0.5f;
		}
	}
}