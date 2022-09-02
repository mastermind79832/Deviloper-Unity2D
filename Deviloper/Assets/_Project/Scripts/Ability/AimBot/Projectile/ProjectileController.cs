using UnityEngine;
using Deviloper.Core;
using Deviloper.Character;

namespace Deviloper.Ability.Aimbot
{
    public class ProjectileController : MonoBehaviour
    {
		[SerializeField] private float m_Damage;
        [SerializeField] private float m_Speed;
        private Vector2 m_Direction = Vector2.zero;


		private void FixedUpdate()
		{
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
				Destroy(gameObject);
			}
		}

		public void SetProperties(float _speed, float _damage, Vector2 _direction)
		{
			m_Speed = _speed;
			m_Damage = _damage;
			m_Direction = _direction;
		}
	}
}
