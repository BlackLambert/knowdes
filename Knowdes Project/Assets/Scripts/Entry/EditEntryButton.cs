using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class EditEntryButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Entry _entry;

        private ContextPanel _contextPanel;

        protected virtual void Start()
		{
            _contextPanel = FindObjectOfType<ContextPanel>();
            _button.onClick.AddListener(onClick);
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(onClick);
        }

        private void onClick()
        {
            _contextPanel.ShowEditPanel(_entry);
        }
    }
}