using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class ClearWeblinkContentButton : MonoBehaviour
    {
        [SerializeField]
        private UriInput _input;
        [SerializeField]
        private Button _clearURLButton;
        [SerializeField]
        private GameObject _buttonObject;
        [SerializeField]
        private GameObject _inactiveButtonObject;

        protected virtual void Start()
        {
            updateView();
            _input.OnUriChanged += updateView;
            _clearURLButton.onClick.AddListener(clearURL);
        }

        protected virtual void OnDestroy()
        {
            _input.OnUriChanged -= updateView;
            _clearURLButton.onClick.RemoveListener(clearURL);
        }

        private void updateView()
        {
            bool activate = !_input.Empty;
            _buttonObject.SetActive(activate);
            _inactiveButtonObject.SetActive(!activate);
        }

        private void clearURL()
        {
            _input.Clear();
        }
    }
}