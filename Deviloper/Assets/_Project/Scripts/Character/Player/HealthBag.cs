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
		public float healAmount; // health/Sec
		public float healingInterval;

		private Coroutine m_HealingRoutine;

		private void Start()
		{
			m_HealthBag = 0;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			HealthPickup healthPickup = collision.GetComponent<HealthPickup>();
			if (healthPickup)
			{
				m_HealthBag += healthPickup.Pickup();
				healthPickup.gameObject.SetActive(false);
			}

			StrongholdController stronghold = collision.GetComponent<StrongholdController>();
			if (stronghold)
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
				yield return new WaitForSeconds(healingInterval);
				stronghold.Heal(GetEffectiveHealing());
			}
		}

		private float GetEffectiveHealing()
		{
			float effectiveHealing = 0;
			if(m_HealthBag < healAmount)
			{
				effectiveHealing = m_HealthBag;
				m_HealthBag = 0;
			}
			else
			{
				effectiveHealing = healAmount;
				m_HealthBag -= healAmount;
			}

			return effectiveHealing;
		}
	}
}
