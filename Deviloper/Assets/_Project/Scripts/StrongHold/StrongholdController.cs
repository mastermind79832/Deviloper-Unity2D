using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;
using System;

namespace Deviloper.Stronghold
{
    public class StrongholdController : MonoBehaviour,IDamageable
    {
        private float m_Health;
		private Collider2D m_Collider;

		private void Start()
		{
			m_Collider = GetComponent<Collider2D>();
		}

		public void TakeDamage(float damage)
		{
            m_Health -= damage;
            CheckHealth();
		}

		private void CheckHealth()
		{
			if(m_Health <= 0)
				m_Collider.enabled = false;
		}

		public void Heal(float amount)
		{
			if (!m_Collider.enabled)
				m_Collider.enabled = true;

			m_Health += amount;
		}
	}
}
