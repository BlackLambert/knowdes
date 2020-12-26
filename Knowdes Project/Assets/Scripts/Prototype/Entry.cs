using System;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class Entry : MonoBehaviour
    {
        [SerializeField]
        private Transform _base = null;
        public Transform Base => _base;

        [SerializeField]
        private Transform _contentHook = null;

        public event Action<Entry> OnDestruct;

        private EntryContent _content = null;
        public EntryContent Content => _content;

        public void MarkForDestruct()
		{
            OnDestruct?.Invoke(this);
        }

        public void SetContent(EntryContent content)
		{
            if (_content != null)
                Destroy(_content.Base.gameObject);

            _content = content;
            content.Base.SetParent(_contentHook, false);
            content.Base.localScale = Vector3.one;
        }
    }
}