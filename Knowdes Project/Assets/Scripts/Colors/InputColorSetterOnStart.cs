using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class InputColorSetterOnStart : MonoBehaviour
    {
        private AppColors _colors;

        [SerializeField]
        private TMP_InputField _target = null;

        protected virtual void Start()
        {
            _colors = FindObjectOfType<AppColors>();
            ColorBlock buttonColors = _target.colors;
            buttonColors.highlightedColor = _colors.Get(AppColors.Type.Highlighted);
            buttonColors.normalColor = _colors.Get(AppColors.Type.Interactable);
            buttonColors.selectedColor = _colors.Get(AppColors.Type.Selected);
            _target.colors = buttonColors;
        }

        protected virtual void Reset()
        {
            _target = GetComponent<TMP_InputField>();
		}
    }
}