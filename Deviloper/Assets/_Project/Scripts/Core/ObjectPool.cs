using System.Collections.Generic;

namespace Deviloper.Core
{
	[System.Serializable]
	public class ObjectPool<T>
	{
		public Queue<T> m_Items;
		public ObjectPool() => m_Items = new Queue<T>();
		public bool IsEmpty() => m_Items.Count == 0;
		public T GetItem() => m_Items.Dequeue();
		public void SetItem(T newItem) => m_Items.Enqueue(newItem);
	}
}
