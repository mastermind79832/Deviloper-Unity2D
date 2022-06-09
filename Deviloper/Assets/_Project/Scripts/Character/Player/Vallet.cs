using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Character
{
    public class Vallet : MonoBehaviour
    {
        private int Coin;

		private void Start()
		{
			Coin = 0;
		}

		public int GetCoin() => Coin;

		public void UseCoin(int amount)
		{
			Coin -= amount;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.CompareTag("Coin")) // check for pickable
			{ 
				// Get from pickable
			}
		}
	}
}
