namespace Deviloper.Pickup
{
	public class CoinPickup : Pickupable<int>
	{
		private void Start()
		{
			pickupType = PickupType.Coin;
		}
	}
}
