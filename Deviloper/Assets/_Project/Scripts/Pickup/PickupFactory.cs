using System.Collections.Generic;
using UnityEngine;
using Deviloper.Core;

namespace Deviloper.Pickup
{
	public class PickupFactory : MonoSingletonGeneric<PickupFactory>
	{

		[System.Serializable]
		public class PickupTypePair
		{
			public PickupType type;
			public Pickupable pickupPrefab;
			public ObjectPool<Pickupable> pool;

			PickupTypePair() =>
				pool = new ObjectPool<Pickupable>();
		}
		public Transform pickupCollection;
		public List<PickupTypePair> pickupPairs;

		public void CreatePickup(int coinAmount, Vector2 position)
		{
			CreatePickup<int>(PickupType.Coin, coinAmount, position);
		}

		public void CreatePickup(float healthAmount, Vector2 position)
		{
			CreatePickup<float>(PickupType.Health, healthAmount, position);
		}

		public void CreatePickup<T>(PickupType type, T item, Vector2 position)
		{
			PickupTypePair typePair = GetTypePair(type);
			Pickupable<T> pickup = GetPickup<T>(typePair);
			SetPickupProperties(item, position, pickup);
		}

		private void SetPickupProperties<T>(T item, Vector2 position, Pickupable<T> pickup)
		{
			pickup.gameObject.SetActive(true);
			pickup.transform.position = position;
			pickup.SetItem(item);
		}

		private Pickupable<T> GetPickup<T>(PickupTypePair typePair)
		{
			if (typePair.pool.IsEmpty())
				return CreatePickup<T>(typePair.pickupPrefab);

			return typePair.pool.GetItem().GetComponent<Pickupable<T>>();
		}

		private PickupTypePair GetTypePair(PickupType type)
		{
			foreach (var pair in pickupPairs)
			{
				if (pair.type == type)
					return pair;
			}
			Debug.LogError("PickupType Prefab Not Found (PickupableFactory:CreatePickup())");
			return default;
		}

		private Pickupable<T> CreatePickup<T>(Pickupable pickupPrefab)
		{
			Pickupable<T> newPickup = Instantiate(pickupPrefab).GetComponent<Pickupable<T>>();
			newPickup.transform.SetParent(pickupCollection);
			return newPickup; 
		}

		public void ReturnPickup(Pickupable pickup)
		{
			PickupTypePair pair = GetTypePair(pickup.pickupType);
			pair.pool.SetItem(pickup);
		}
	}
}
