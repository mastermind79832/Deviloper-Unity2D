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
			m_UpgradeUI.InitializeUI(m_AbilityName, m_Detail_1, m_Detail_2, m_Detail_3, UpgradeAll);
			Character.Vallet.OnMoneyUpdate += IsUpgradePossible;
			UpdateUI();
		}

		protected void UpgradeAll()
		{
			if (!IsMoneyEnough())
				return;
			Character.Vallet.UseCoin(m_UpgradeAmount);
			Upgrade();
			m_UpgradeAmount *= 2 ;
			m_Level ++;
			UpdateUI();
		}

		protected abstract void Upgrade();
		protected abstract void UpdateUI();
	
		private void IsUpgradePossible(int money)
		{
			m_UpgradeUI.SetInteractable(IsMoneyEnough());
		}

		private bool IsMoneyEnough() =>  Character.Vallet.GetCoin() >= m_UpgradeAmount;

		private void OnDisable()
		{
			Character.Vallet.OnMoneyUpdate -= IsUpgradePossible;
		}
	}
}
