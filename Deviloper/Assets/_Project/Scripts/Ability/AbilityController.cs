using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Ability
{
    public abstract class AbilityController : MonoBehaviour
    {
		private int m_Level;
		[Header("Money")]
		[SerializeField] private float m_UpgradeAmount;

		[Header("UI Properties")]
		[SerializeField] private string m_Name;
		[SerializeField] private string m_Detail_1;
		[SerializeField] private string m_Detail_2;
		[SerializeField] private string m_Detail_3;
		[SerializeField] private UI.UpgradeUI m_UpgradeUI;

		protected virtual void Start()
		{
			m_UpgradeUI = UI.UiController.Instance.UpgradeUI;
			m_UpgradeUI.InitializeUI(m_Name, m_Detail_1, m_Detail_2, m_Detail_3, Upgrade);	
		}

		protected virtual void Upgrade()
		{

		}
	}
}
