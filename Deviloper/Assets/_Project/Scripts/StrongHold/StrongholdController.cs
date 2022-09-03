using System;
using UnityEngine;
using Deviloper.Core;

namespace Deviloper.Stronghold
{
    public class StrongholdController : MonoBehaviour,IDamageable
    {
		[SerializeField] private float m_MaxHealth;
        private float m_Health;
		private Collider2D m_Collider;

		private Action<float> m_OnHeathUpdate;

		public bool IsDefenceEnabled { get; private set; }
			
		private void Start()
		{
			m_Collider = GetComponent<Collider2D>();
			m_Health = m_MaxHealth;
			IsDefenceEnabled = true;
			UI.UiController.Instance.PlayerDetailUI.SetMaxHealthU(m_MaxHealth);
			m_OnHeathUpdate = UI.UiController.Instance.PlayerDetailUI.RefereshHealthUI;
			m_OnHeathUpdate(m_Health);
		}

		public void TakeDamage(float damage)
		{
            m_Health -= damage;
            CheckHealth();
		}

		private void CheckHealth()
		{
			if(m_Health <= 0)
			{
				m_Health = 0;
				IsDefenceEnabled = false;
			}
			m_OnHeathUpdate(m_Health);
		}

		public void Heal(float amount)
		{
			if (!IsDefenceEnabled)
				IsDefenceEnabled = true;

			m_Health += amount;
			if (IsHealthFull())
				m_Health = m_MaxHealth;

			m_OnHeathUpdate(m_Health);
		}

		public bool IsHealthFull() => m_Health >= m_MaxHealth;
	}
}
