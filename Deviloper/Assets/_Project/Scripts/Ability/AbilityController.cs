using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Ability
{
    public abstract class AbilityController : MonoBehaviour
    {
		[SerializeField] protected int m_Level;
		[Header("Money")]
		[SerializeField] protected int m_UpgradeAmount;

		[Header("UI Properties")]
		protected string m_AbilityName;
		protected string m_Detail_1;
		protected string m_Detail_2;
		protected string m_Detail_3;
		[SerializeField] protected UI.UpgradeUI m_UpgradeUI;

		protected virtual void Start()
		{
			m_UpgradeUI.InitializeUI(m_AbilityName, m_Detail_1, m_Detail_2, m_Detail_3, Upgrade);
			Character.Vallet.OnMoneyUpdate += IsUpgradePossible;
			UpdateUI();
		}

		protected virtual void Upgrade()
		{
			Character.Vallet.UseCoin(m_UpgradeAmount);
			m_UpgradeAmount *= 2 ;
			m_Level ++;
			UpdateUI();
		}

		protected abstract void UpdateUI();
	
		private void IsUpgradePossible(int money)
		{
			m_UpgradeUI.SetInteractable(money >= m_UpgradeAmount);
		}

	}
}
