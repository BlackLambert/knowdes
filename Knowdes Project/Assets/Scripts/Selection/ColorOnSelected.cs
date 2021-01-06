using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class ColorOnSelected : MonoBehaviour
    {
        [SerializeField]
        private Graphic _graphic = null;
        [SerializeField]
        private UISelectable _selectable = null;

        private AppColors _colors;

        protected virtual void Start()
		{

            _colors = FindObjectOfType<AppColors>();
            _graphic.color = _colors.Get(AppColors.Type.Interactable);
            _selectable.OnSelected += updateColor;
            _selectable.OnDeselected += updateColor;
            updateColor();
        }

        protected virtual void OnDestroy()
		{

            _selectable.OnSelected -= updateColor;
            _selectable.OnDeselected -= updateColor;
        }

		private void updateColor()
		{
            _graphic.color = _selectable.Selected ? _colors.Get(AppColors.Type.Selected) : 
                _colors.Get(AppColors.Type.InteractionElementBackground);
        }
	}
}