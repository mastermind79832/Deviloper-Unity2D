using UnityEngine;
using Deviloper.Core;
using Deviloper.Character;
using System;

namespace Deviloper.Ability.Aimbot
{
    public class ProjectileController : MonoBehaviour
    {
		[SerializeField] private float m_Damage;
        [SerializeField] private float m_Speed;
		[SerializeField] private float m_MaxTimer;
        private Vector2 m_Direction = Vector2.zero;

		public Action<ProjectileController> OnDisableBullet;

		private float m_Timer;
	

		private void OnEnable()
		{
			m_Timer = m_MaxTimer;
		}

		private void FixedUpdate()
		{
			m_Timer -= Time.fixedDeltaTime;
			if (m_Timer <= 0)
				gameObject.SetActive(false);
			MoveProjectile();
		}

		private void MoveProjectile()
		{
			transform.position += m_Speed * Time.deltaTime * (Vector3)m_Direction;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.GetComponent<EnemyController>() != null)
			{
				collision.GetComponent<IDamageable>().TakeDamage(m_Damage);
				gameObject.SetActive(false);
			}
		}

		public void SetProperties(float _speed, float _damage, Vector2 _direction)
		{
			m_Speed = _speed;
			m_Damage = _damage;
			m_Direction = _direction;
		}

		private void OnDisable()
		{
			OnDisableBullet(this);
		}
	}
}
