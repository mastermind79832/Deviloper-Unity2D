using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Deviloper.UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_AbilityNameText;
        [SerializeField] private TextMeshProUGUI m_Detail_1Text;
        [SerializeField] private TextMeshProUGUI m_Detail_2Text;
        [SerializeField] private TextMeshProUGUI m_Detail_3Text;

        [SerializeField] private TextMeshProUGUI m_MoneyAmount;
        [SerializeField] private Button m_MoneyButton;

		private string m_AbilityName;
		private string m_Detail_1;
		private string m_Detail_2;
		private string m_Detail_3;
		private const string m_MoneySymbol = "$";

		private Action m_OnUpgrade;

		public void OnMoneyButtonPressed()
		{
			m_OnUpgrade();
		}

		public void InitializeUI(string abilityName, string detail_1, string detail_2, string detail_3, Action Upgrade)
		{
			m_AbilityName = abilityName;
			m_Detail_1 = detail_1;
			m_Detail_2 = detail_2;
			m_Detail_3 = detail_3;
			m_OnUpgrade = Upgrade;
		}

		public void RefreshUI(int abilityLvl, float detail_1, float detail_2, float detail_3, float amount)
		{
			m_AbilityNameText.text = $"{m_AbilityName} Lv.{abilityLvl}";
			m_Detail_1Text.text = $"{m_Detail_1} : {detail_1}";
			m_Detail_2Text.text = $"{m_Detail_2} : {detail_2}";
			m_Detail_3Text.text = $"{m_Detail_3} : {detail_3}";
			SetMoney(amount);
		}

		private void SetMoney(float amount) => m_MoneyAmount.text = $"{m_MoneySymbol}{amount}";
	

		public void SetInteractable(bool isInteractable)
		{
            m_MoneyButton.interactable = isInteractable;
		}
    }
}
