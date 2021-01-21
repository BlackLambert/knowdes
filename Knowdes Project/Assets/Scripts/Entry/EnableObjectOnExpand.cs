using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class EnableObjectOnExpand : MonoBehaviour
    {
        [SerializeField]
        private GameObject _objectToControl;
        [SerializeField]
        private EntryVolume _volume;

        [SerializeField]
        private bool _activate = true;

        protected virtual void Start()
		{
            if(_volume == null)
                _volume = GetComponentInParent<EntryVolume>();
            updateView();
            _volume.OnExpanded += updateView;
        }

        protected virtual void OnDestroy()
		{
            _volume.OnExpanded -= updateView;
        }

        private void updateView()
		{
            _objectToControl.SetActive(_activate && _volume.Expanded || !_activate && !_volume.Expanded);

        }
    }
}