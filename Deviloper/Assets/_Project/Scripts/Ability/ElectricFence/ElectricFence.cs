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
			m_AbilityName = "E-Fence";
			m_Detail_1 = "Damage";
			m_Detail_2 = "Slow Down";
			m_Detail_3 = "";
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

		protected override void UpdateUI()
		{
			m_UpgradeUI.RefreshUI(m_Level, m_Damage, m_SlowDownMultiplier, 0, m_UpgradeAmount);
		}

		protected override void Upgrade()
		{
			m_Damage += 0.3f;
			m_SlowDownMultiplier += 0.2f;
			base.Upgrade();
		}
	}
}