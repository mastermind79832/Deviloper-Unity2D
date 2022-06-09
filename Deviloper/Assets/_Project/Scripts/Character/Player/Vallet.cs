using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Pickup;

namespace Deviloper.Character
{
    public class Vallet : MonoBehaviour
    {
        private int vallet;

		private void Start()
		{
			vallet = 0;
		}

		public int GetCoin() => vallet;

		public void UseCoin(int amount)
		{
			vallet -= amount;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			CoinPickup coinPickup = collision.GetComponent<CoinPickup>();
			if (coinPickup)
			{
				vallet += coinPickup.Pickup();
				coinPickup.gameObject.SetActive(false);
				return;
			}
		}
	}
}
