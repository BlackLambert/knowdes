using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class ButtonColorSetterOnStart : MonoBehaviour
    {
        private AppColors _colors;

        [SerializeField]
        private Button _target = null;

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
            _target = GetComponent<Button>();

        }
    }
}