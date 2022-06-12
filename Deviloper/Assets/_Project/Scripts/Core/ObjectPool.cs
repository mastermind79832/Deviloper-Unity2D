using System.Collections.Generic;

namespace Deviloper.Pickup
{
	public class ObjectPool<T>
	{
		public Queue<T> Items;
		public ObjectPool() => Items = new Queue<T>();
		public bool IsEmpty() => Items.Count == 0;
		public T GetItem() => Items.Dequeue();
		public void SetItem(T newItem) => Items.Enqueue(newItem);
	}
}
