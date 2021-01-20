using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class RestrictTextPreferedHeightOnEntryCollapse : MonoBehaviour
    {
        [SerializeField]
        private TextLayoutElement _layoutElement = null;
        [SerializeField]
        private float _restrictedHeight = 100f;
        [SerializeField]
        private EntryVolume _entryVolume = null;

        private TMPro.TextOverflowModes _defaultOverflowMode;


        protected virtual void Start()
        {
            findEntryVolume();
            _entryVolume.OnExpanded += checkRestrict;
            checkRestrict();
        }

        protected virtual void OnDestroy()
        {
            if (_entryVolume != null)
                _entryVolume.OnExpanded -= checkRestrict;
        }

        protected virtual void Reset()
        {
            _entryVolume = GetComponentInParent<EntryVolume>();
            _layoutElement = GetComponent<TextLayoutElement>();
        }

        //TODO Not very elegant! Other solution needed
        private void findEntryVolume()
        {
            if (_entryVolume == null)
                _entryVolume = GetComponentInParent<EntryVolume>();
            if (_entryVolume == null)
                _entryVolume = GetComponent<EntryVolume>();
            if (_entryVolume == null)
                throw new MissingComponentException($"There has to be an instance of the {nameof(EntryVolume)} component on the object or its parents");
        }

        private void checkRestrict()
        {
            if (_entryVolume.Expanded)
            {
                _layoutElement.ResetMaxHeight();
                _layoutElement.Text.overflowMode = _defaultOverflowMode;
            }
            else
            {
                _defaultOverflowMode = _layoutElement.Text.overflowMode;
                _layoutElement.MaxHeight = _restrictedHeight;
                _layoutElement.Text.overflowMode = TMPro.TextOverflowModes.Ellipsis;
            }
        }
    }
}
