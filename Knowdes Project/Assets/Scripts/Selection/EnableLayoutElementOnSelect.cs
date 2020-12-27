using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class EnableLayoutElementOnSelect : MonoBehaviour
    {
        [SerializeField]
        private LayoutElement _layoutElement = null;
        [SerializeField]
        private bool _activate = false;
        [SerializeField]
        private EntryContent _entryContent = null;

        public event Action OnChanged;
        

        protected virtual void Start()
		{
            addSelectedListener(_entryContent.Selectable);
            checkActivate();
            _entryContent.OnSelectableChange += onSelectableChange;
        }

		protected virtual void OnDestroy()
		{
            removeSelectedListener(_entryContent.Selectable);
            _entryContent.OnSelectableChange -= onSelectableChange;
        }

        protected virtual void Reset()
		{
            _entryContent = GetComponentInParent<EntryContent>();
            _layoutElement = GetComponent<LayoutElement>();
		}


        private void onSelectableChange(UISelectable former, UISelectable newEntry)
        {
            if(former != null)
                removeSelectedListener(former);
            addSelectedListener(newEntry);
        }

        private void addSelectedListener(UISelectable selectable)
		{
            _entryContent.Selectable.OnSelected += checkActivate;
            _entryContent.Selectable.OnDeselected += checkActivate;
        }

        private void removeSelectedListener(UISelectable selectable)
		{
            _entryContent.Selectable.OnSelected -= checkActivate;
            _entryContent.Selectable.OnDeselected -= checkActivate;
        }

        private void checkActivate()
        {
            bool enable = _activate && _entryContent.Selectable.Selected ||
                !_activate && !_entryContent.Selectable.Selected;
            if (_layoutElement.enabled == enable)
                return;
            _layoutElement.enabled = enable;
            OnChanged?.Invoke();
        }
    }
}