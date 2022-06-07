using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deviloper.Core
{
    public class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoSingletonGeneric<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

		protected virtual void Awake()
		{
			if (instance == null)
				CreateInstance();
			else
			{
				Destroy(this);
				return;
			}
		}

		protected virtual void CreateInstance()
		{
			instance = (T)this;
		}
	}
}
