namespace Deviloper.Pickup
{
	public class HealthPickup : Pickupable<float>
	{
		private void Start()
		{
			pickupType = PickupType.Health;
		}
	}
}
