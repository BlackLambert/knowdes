using System;
using UnityEngine;

namespace Knowdes
{
    public class Entry : MonoBehaviour
    {
        [SerializeField]
        private Transform _base = null;
        public Transform Base => _base;

        public event Action<Entry> OnDestruct;

        public void MarkForDestruct()
		{
            OnDestruct?.Invoke(this);

        }
    }
}