using System;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class Entry : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _base = null;
        public RectTransform Base => _base;

        [SerializeField]
        private EntryVolume _content = null;
        public EntryVolume Volume => _content;

        [SerializeField]
        private UISelectable _selectable = null;
        public UISelectable Selectable => _selectable;
        public event Action<Entry> OnMarkedForDestruct;

        private bool _pending = true;
        public event Action OnPendingChanged;
        public bool Pending
		{
            get => _pending;
            set
			{
                _pending = value;
                OnPendingChanged?.Invoke();

            }
		}

        public EntryData Data => Volume.Data;

        public void MarkForDestruct()
		{
            OnMarkedForDestruct?.Invoke(this);
        }
    }
}