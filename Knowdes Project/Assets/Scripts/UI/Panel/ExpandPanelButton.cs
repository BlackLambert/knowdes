using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class ExpandPanelButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Panel _panel;

        protected virtual void Start()
        {
            _button.onClick.AddListener(onClick);

        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(onClick);
        }

        protected virtual void Reset()
        {
            _button = GetComponent<Button>();
            _panel = GetComponent<Panel>();
        }

        private void onClick()
        {
            _panel.Expand();
        }
    }
}