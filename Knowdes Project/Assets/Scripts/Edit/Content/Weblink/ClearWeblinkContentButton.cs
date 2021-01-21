using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class ClearWeblinkContentButton : MonoBehaviour
    {
        [SerializeField]
        private WeblinkContentEditor _editor;
        [SerializeField]
        private Button _clearURLButton;
        [SerializeField]
        private GameObject _buttonObject;
        [SerializeField]
        private GameObject _inactiveButtonObject;

        protected virtual void Start()
        {
            updateView();
            _editor.OnChanged += updateView;
            _clearURLButton.onClick.AddListener(clearURL);
        }

        protected virtual void OnDestroy()
        {
            _editor.OnChanged -= updateView;
            _clearURLButton.onClick.RemoveListener(clearURL);
        }

        private void updateView()
        {
            bool activate = _editor.Data != null && !_editor.Data.Empty;
            _buttonObject.SetActive(activate);
            _inactiveButtonObject.SetActive(!activate);
        }

        private void clearURL()
        {
            _editor.HostInput.text = null;
        }
    }
}