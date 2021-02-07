using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class OpenWeblinkContentButton : MonoBehaviour
    {
        [SerializeField]
        private UriInput _editor;
        [SerializeField]
        private Button _openURLButton;
        [SerializeField]
        private GameObject _buttonObject;
        [SerializeField]
        private GameObject _inactiveButtonObject;

        protected virtual void Start()
		{
            updateView();
            _editor.OnUriChanged += updateView;
            _openURLButton.onClick.AddListener(openURL);
        }

        protected virtual void OnDestroy()
		{
            _editor.OnUriChanged -= updateView;
            _openURLButton.onClick.RemoveListener(openURL);
        }

        private void updateView()
		{
            bool activate = _editor.Uri != null;
            _buttonObject.SetActive(activate);
            _inactiveButtonObject.SetActive(!activate);
        }

        private void openURL()
		{
            Application.OpenURL(_editor.Uri.AbsoluteUri);
        }
    }
}