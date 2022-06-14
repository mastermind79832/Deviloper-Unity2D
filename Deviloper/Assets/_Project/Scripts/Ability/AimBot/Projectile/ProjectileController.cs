using UnityEngine;
using Deviloper.Core;
using Deviloper.Character;

namespace Deviloper.Ability.Aimbot
{
    public class ProjectileController : MonoBehaviour
    {
        public float speed;
        public Vector2 direction = Vector2.zero;

		public float damage;

		private void FixedUpdate()
		{
			MoveProjectile();
		}

		private void MoveProjectile()
		{
			transform.position += speed * Time.deltaTime * (Vector3)direction;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.GetComponent<EnemyController>() != null)
			{
				collision.GetComponent<IDamageable>().TakeDamage(damage);
				Destroy(gameObject);
			}
		}

		public void SetProperties(float _speed, float _damage, Vector2 _direction)
		{
			speed = _speed;
			damage = _damage;
			direction = _direction;
		}
	}
}
