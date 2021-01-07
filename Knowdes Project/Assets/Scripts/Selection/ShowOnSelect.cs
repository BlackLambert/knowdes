using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ShowOnSelect : MonoBehaviour
    {
        [SerializeField]
        private UISelectable _selectable = null;
        [SerializeField]
        private bool _show = true;
        [SerializeField]
        private GameObject _target = null;

        private bool ShallShow => _selectable.Selected && _show || !_selectable.Selected && !_show;

        protected virtual void Start()
		{
            _target.SetActive(ShallShow);
            _selectable.OnSelected += onSelect;
            _selectable.OnDeselected += onDeselect;
        }

        protected virtual void OnDestroy()
        {
            _selectable.OnSelected -= onSelect;
            _selectable.OnDeselected -= onDeselect;
        }

        private void onDeselect()
		{
            _target.SetActive(ShallShow);
        }

		private void onSelect()
		{
            _target.SetActive(ShallShow);
        }
    }
}