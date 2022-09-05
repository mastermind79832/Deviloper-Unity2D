using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Pickup;
using Deviloper.Stronghold;

namespace Deviloper.Character
{
    public class HealthBag : MonoBehaviour
    {
		private float m_HealthBag;
		[SerializeField] private float m_HealAmount; // health/Sec
		[SerializeField] private float m_HealingInterval;

		private bool m_InStrongHold;
		private float m_HealTimer;
		private StrongholdController stronghold;
		public static event Action<float> OnHealthUpdate;

		private void Start()
		{
			stronghold = StrongholdController.Instance;
			m_HealthBag = 0;
			m_InStrongHold = false;
			OnHealthUpdate(m_HealthBag);
		}

		private void Update()
		{
			if (IsHealPossible())
			{
				DecreaseTimer();
				if (m_HealTimer < 0)
				{
					m_HealTimer = m_HealingInterval;
					stronghold.Heal(GetEffectiveHealing());
				}
			}
		}

		private bool IsHealPossible() => m_InStrongHold && m_HealthBag > 0 && !stronghold.IsHealthFull();
		private void DecreaseTimer() =>	m_HealTimer -= Time.deltaTime;		

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out HealthPickup healthPickup))
			{
				m_HealthBag += healthPickup.Pickup();
				OnHealthUpdate(m_HealthBag);
				healthPickup.gameObject.SetActive(false);
			}

			if (collision.TryGetComponent(out StrongholdController stronghold))
				m_InStrongHold = true;
			
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out StrongholdController stronghold))
				m_InStrongHold = false;
		}

		private float GetEffectiveHealing()
		{
			float effectiveHealing;
			if (m_HealthBag < m_HealAmount)
			{
				effectiveHealing = m_HealthBag;
				m_HealthBag = 0;
			}
			else
			{
				effectiveHealing = m_HealAmount;
				m_HealthBag -= m_HealAmount;
			}

			OnHealthUpdate(m_HealthBag);

			return effectiveHealing;
		}
	}
}
