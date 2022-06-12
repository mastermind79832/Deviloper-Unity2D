using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Pickup;

namespace Deviloper.Character
{
    public class Vallet : MonoBehaviour
    {
        private int m_Vallet;

		private void Start()
		{
			m_Vallet = 0;
		}

		public int GetCoin() => m_Vallet;

		public void UseCoin(int amount)
		{
			m_Vallet -= amount;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			CoinPickup coinPickup = collision.GetComponent<CoinPickup>();
			if (coinPickup)
			{
				m_Vallet += coinPickup.Pickup();
				coinPickup.gameObject.SetActive(false);
				return;
			}
		}
	}
}
