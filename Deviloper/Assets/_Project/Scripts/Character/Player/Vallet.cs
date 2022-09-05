using System;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Pickup;

namespace Deviloper.Character
{
    public class Vallet : MonoBehaviour
    {
        private static int m_Vallet;
		public static event Action<int> OnMoneyUpdate;
		public static Action<int> OnMoneyUse;

		private void Start()
		{
			m_Vallet = 0;
			OnMoneyUpdate?.Invoke(m_Vallet);
		}

		public static int GetCoin() => m_Vallet;

		public static void UseCoin(int amount)
		{
			m_Vallet -= amount;
			OnMoneyUpdate(m_Vallet);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			CoinPickup coinPickup = collision.GetComponent<CoinPickup>();
			if (coinPickup)
			{
				m_Vallet += coinPickup.Pickup();
				OnMoneyUpdate(m_Vallet);
				coinPickup.gameObject.SetActive(false);
				return;
			}
		}
	}
}
