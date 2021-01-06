using System;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class EnableLayoutElementOnContentExpand : MonoBehaviour
    {
        [SerializeField]
        private LayoutElement _layoutElement = null;
        [SerializeField]
        private bool _activate = false;
        [SerializeField]
        private EntryVolume _entryVolume = null;
        

        protected virtual void Start()
		{
            findEntryContent();
            _entryVolume.OnExpanded += checkActivate;
            checkActivate();
        }

		protected virtual void OnDestroy()
		{
            if(_entryVolume != null)
                _entryVolume.OnExpanded -= checkActivate;
        }

        protected virtual void Reset()
		{
            _entryVolume = GetComponentInParent<EntryVolume>();
            _layoutElement = GetComponent<LayoutElement>();
		}

        private void findEntryContent()
		{
            if (_entryVolume == null)
                _entryVolume = GetComponentInParent<EntryVolume>();
            if (_entryVolume == null)
                _entryVolume = GetComponent<EntryVolume>();
            if (_entryVolume == null)
                throw new Exception($"There has to be an instance of the {nameof(EntryVolume)} component on the object or its parents");
        }

        private void checkActivate()
        {
            bool enable = _activate && _entryVolume.Expanded ||
                !_activate && !_entryVolume.Expanded;
            if (_layoutElement.enabled == enable)
                return;
            _layoutElement.enabled = enable;
        }
    }
}