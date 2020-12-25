using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class RemoveEntryButton : MonoBehaviour
    {
        [SerializeField]
        private Entry _entry = null;
        [SerializeField]
        private Button _button = null;

        protected virtual void Start()
		{
            _button.onClick.AddListener(remove);

        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(remove);
        }

        private void remove()
        {
            _entry.MarkForDestruct();
        }
    }
}