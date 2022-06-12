using UnityEngine;

namespace Deviloper.Projectile
{
    public class ProjectileController : MonoBehaviour
    {
        public float moveSpeed;
        public Vector2 direction;
		private void FixedUpdate()
		{
			MoveProjectile();
		}

		private void MoveProjectile()
		{
			transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
		}
	}
}
