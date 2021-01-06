using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public abstract class ExpandEntryVolumeOnSelect : MonoBehaviour
    {
        protected abstract EntryVolume EntryContent { get; }
        [SerializeField]
        private UISelectable _selectable = null;
        [SerializeField]
        private bool _expand = true;

        protected virtual void Start()
		{
            _selectable.OnSelected += checkExpand;
            _selectable.OnDeselected += checkExpand;
            checkExpand();
        }

        protected virtual void OnDestroy()
		{
            _selectable.OnSelected -= checkExpand;
            _selectable.OnDeselected -= checkExpand;
        }

        private void checkExpand()
		{
            EntryContent.Expanded = _expand && _selectable.Selected || !_expand && !_selectable.Selected;
        }
    }
}