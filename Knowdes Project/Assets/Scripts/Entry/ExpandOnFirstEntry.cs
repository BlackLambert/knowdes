using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class ExpandOnFirstEntry : MonoBehaviour
    {
        [SerializeField]
        private EntriesList _entries = null;
        [SerializeField]
        private Panel _target = null;
        [SerializeField]
        private bool _expand = true;

        protected virtual void Start()
		{
            checkExpand();
            _entries.OnCountChanged += checkExpand;
        }

        protected virtual void OnDestroy()
		{
            _entries.OnCountChanged -= checkExpand;
        }

        private void checkExpand()
		{
            bool noEntries = _entries.Count == 0;
            if (_target.Collapsed && (!noEntries && _expand || noEntries && !_expand))
                _target.Expand();
            else if (!_target.Collapsed && (!noEntries && !_expand || noEntries && _expand))
                _target.Collapse();
		}
    }
}