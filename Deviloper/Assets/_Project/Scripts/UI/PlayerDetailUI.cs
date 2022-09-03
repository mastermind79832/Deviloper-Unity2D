using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Deviloper.UI
{
    public class PlayerDetailUI : MonoBehaviour
    {
		[Header("Health")]
		[SerializeField] private Slider m_HealthSlider;
		private float m_MaxHealth;

        [Header("Collectibles")]
        [SerializeField] private TextMeshProUGUI m_MoneyText;
        [SerializeField] private TextMeshProUGUI m_HealthText;        

        public void SetMaxHealthU(float value) => m_MaxHealth = value;
        public void RefereshHealthUI(float currentValue) => m_HealthSlider.value = currentValue / m_MaxHealth;
        public void RefreshHealthPickUpAmount(float health) => m_HealthText.text = health.ToString();
        public void RefreshMoneyPickUpAmount(int money) => m_MoneyText.text = money.ToString();
        
    }
}
