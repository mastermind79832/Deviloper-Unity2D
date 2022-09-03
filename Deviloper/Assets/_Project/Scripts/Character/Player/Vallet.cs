using System;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Pickup;

namespace Deviloper.Character
{
    public class Vallet : MonoBehaviour
    {
        private int m_Vallet;
		private Action<int> m_OnMoneyUpdate;

		private void Start()
		{
			m_Vallet = 0;
			m_OnMoneyUpdate = UI.UiController.Instance.PlayerDetailUI.RefreshMoneyPickUpAmount;
			m_OnMoneyUpdate(m_Vallet);
		}

		public int GetCoin() => m_Vallet;

		public void UseCoin(int amount)
		{
			m_Vallet -= amount;
			m_OnMoneyUpdate(m_Vallet);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			CoinPickup coinPickup = collision.GetComponent<CoinPickup>();
			if (coinPickup)
			{
				m_Vallet += coinPickup.Pickup();
				m_OnMoneyUpdate(m_Vallet);
				coinPickup.gameObject.SetActive(false);
				return;
			}
		}
	}
}
