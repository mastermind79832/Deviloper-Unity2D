using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Pickup;

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

			if (collision.CompareTag("Stronghold"))
			{
				if (healthBag > 0)
				{
					routine = StartCoroutine(HealStronghold());
				}
			}
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.CompareTag("Stronghold"))
			{
				StopCoroutine(routine);
			}
		}

		IEnumerator HealStronghold()
		{

			while(healthBag > 0)
			{
				//stronghold.increaseHealth(healRate.Key)
				yield return new WaitForSeconds(1f);
			}

		}
	}
}
