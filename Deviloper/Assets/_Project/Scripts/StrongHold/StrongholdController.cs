using UnityEngine;
using Deviloper.Core;

namespace Deviloper.Stronghold
{
    public class StrongholdController : MonoBehaviour,IDamageable
    {
		public float maxHealth;
        private float m_Health;
		private Collider2D m_Collider;

		public bool isDefenceEnabled { get; private set; }

		private void Start()
		{
			m_Collider = GetComponent<Collider2D>();
			m_Health = maxHealth;
			isDefenceEnabled = true;
		}

		public void TakeDamage(float damage)
		{
            m_Health -= damage;
            CheckHealth();
		}

		private void CheckHealth()
		{
			if(m_Health <= 0)
				isDefenceEnabled = false;
		}

		public void Heal(float amount)
		{
			if (!isDefenceEnabled)
				isDefenceEnabled = true;

			m_Health += amount;
			if (IsHealthFull())
				m_Health = maxHealth;
		}

		public bool IsHealthFull() => m_Health >= maxHealth;
	}
}
