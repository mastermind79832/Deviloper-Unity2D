using System.Collections;
using UnityEngine;

namespace Deviloper.Pickup
{
	public class Pickupable : MonoBehaviour
    {
		[HideInInspector]
        public PickupType pickupType;
		public float aliveTime;

		private Coroutine disableCoutdown; 
		private void OnEnable()
		{
			disableCoutdown = StartCoroutine(DisableTimer());
		}

		IEnumerator DisableTimer()
		{
			yield return new WaitForSeconds(aliveTime);
			gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			StopCoroutine(disableCoutdown);
			PickupFactory.Instance.ReturnPickup(this);
		}
    }

    public class Pickupable<T> : Pickupable 
	{
        protected T item;
        public T Pickup() => item;
		public void SetItem(T value) => item = value;
	}
}
