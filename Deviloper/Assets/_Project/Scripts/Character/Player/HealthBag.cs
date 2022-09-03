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

		private Coroutine m_HealingRoutine;
		public static event Action<float> OnHealthUpdate;

		private void Start()
		{
			m_HealthBag = 0;
			OnHealthUpdate(m_HealthBag);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out HealthPickup healthPickup))
			{
				m_HealthBag += healthPickup.Pickup();
				OnHealthUpdate(m_HealthBag);
				healthPickup.gameObject.SetActive(false);
			}

			if (collision.TryGetComponent(out StrongholdController stronghold))
			{
				if (m_HealthBag > 0)
				{
					m_HealingRoutine = StartCoroutine(HealStronghold(stronghold));
				}
			}
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			StrongholdController stronghold = collision.GetComponent<StrongholdController>();
			if (stronghold && m_HealingRoutine!= null)
			{
				StopCoroutine(m_HealingRoutine);
			}
		}

		IEnumerator HealStronghold(StrongholdController stronghold)
		{
			while(m_HealthBag > 0 & !stronghold.IsHealthFull())
			{
				yield return new WaitForSeconds(m_HealingInterval);
				stronghold.Heal(GetEffectiveHealing());
			}
		}

		private float GetEffectiveHealing()
		{
			float effectiveHealing = 0;
			if(m_HealthBag < m_HealAmount)
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
