using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Pickup;
using Deviloper.Stronghold;

namespace Deviloper.Character
{
    public class BloodBag : MonoBehaviour
    {
		private float healthBag;
		public float healRate; // health/Sec

		private Coroutine routine;
		private List<Coroutine> coroutines = new List<Coroutine>();

		private void Start()
		{
			healthBag = 0;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			HealthPickup healthPickup = collision.GetComponent<HealthPickup>();
			if (healthPickup)
			{
				healthBag += healthPickup.Pickup();
				healthPickup.gameObject.SetActive(false);
				return;
			}

			StrongholdController stronghold = collision.GetComponent<StrongholdController>();
			if (stronghold)
			{
				if (healthBag > 0)
				{
					routine = StartCoroutine(HealStronghold(stronghold));
				}
			}
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			StrongholdController stronghold = collision.GetComponent<StrongholdController>();
			if (stronghold)
			{
				StopCoroutine(routine);
			}
		}

		IEnumerator HealStronghold(StrongholdController stronghold)
		{

			while(healthBag > 0)
			{
				stronghold.Heal(healRate);
				yield return new WaitForSeconds(1f);
			}

		}
	}
}
