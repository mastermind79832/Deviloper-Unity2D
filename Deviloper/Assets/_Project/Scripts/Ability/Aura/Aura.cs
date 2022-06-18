using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;

namespace Deviloper.Ability.Aura
{
    public class Aura : MonoBehaviour
    {
        public float damageRate;

		private void OnTriggerStay2D(Collider2D other)
		{
			if(other.TryGetComponent(out IDamageable enemy))
			{
				enemy.TakeDamage(damageRate/10);
			}
		}
	}
}