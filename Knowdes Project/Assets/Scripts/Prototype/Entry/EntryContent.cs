using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EntryContent : MonoBehaviour
    {
        [SerializeField]
        private Transform _base;
        public Transform Base => _base;

        private UISelectable _selectable = null;
        public UISelectable Selectable
		{
            get => _selectable;
			set
			{
                UISelectable former = _selectable;
                _selectable = value;
                OnSelectableChange?.Invoke(former, _selectable);
			}
		}

        [SerializeField]
        private EnableLayoutElementOnSelect _layoutElementEnabler = null;



        public event Action<UISelectable, UISelectable> OnSelectableChange;
        public event Action OnLayoutChanged;

        protected virtual void Start()
		{
            _layoutElementEnabler.OnChanged += invokeLayoutChanged;

        }

        protected virtual void OnDestroy()
		{
            _layoutElementEnabler.OnChanged -= invokeLayoutChanged;
        }

		private void invokeLayoutChanged()
		{
            OnLayoutChanged?.Invoke();

        }
	}
}