using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class OpenWeblinkContent : MonoBehaviour
    {
        [SerializeField]
        private WeblinkContent _weblinkContent;
        [SerializeField]
        private Button _button;

        protected virtual void Start()
		{
            _button.onClick.AddListener(onClick);
            _weblinkContent.Data.OnUrlChanged += updateView;
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(onClick);
            _weblinkContent.Data.OnUrlChanged -= updateView;
        }

		private void onClick()
		{
            Application.OpenURL(_weblinkContent.Data.UrlString);
        }

        private void updateView()
        {
            _button.interactable = !string.IsNullOrEmpty(_weblinkContent.Data.UrlString);
        }
    }
}