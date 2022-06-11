using UnityEngine;
using Deviloper.Core;

namespace Deviloper.Stronghold
{
    public class StrongholdController : MonoBehaviour,IDamageable
    {
		public float maxHealth;
        private float m_Health;
		private Collider2D m_Collider;

		private void Start()
		{
			m_Collider = GetComponent<Collider2D>();
			m_Health = maxHealth;
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
			if (IsHealthFull())
				m_Health = maxHealth;
		}

		public bool IsHealthFull() => m_Health >= maxHealth;
	}
}
