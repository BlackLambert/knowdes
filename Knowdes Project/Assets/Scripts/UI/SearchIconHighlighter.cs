using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class SearchIconHighlighter : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _inputField = null;
        [SerializeField]
        private Image _icon = null;

        private AppColors _appColors;


        protected virtual void Start()
		{
            _appColors = FindObjectOfType<AppColors>();
            _inputField.onSelect.AddListener(onSelect);
            _inputField.onDeselect.AddListener(onDeselect);
        }

		protected virtual void OnDestroy()
		{
            _inputField.onSelect.RemoveListener(onSelect);
            _inputField.onDeselect.RemoveListener(onDeselect);
        }

        private void onSelect(string arg0)
        {
            _icon.color = _appColors.Get(AppColors.Type.Selected);
        }

        private void onDeselect(string arg0)
        {
            _icon.color = _appColors.Get(AppColors.Type.Interactable);
        }
    }
}