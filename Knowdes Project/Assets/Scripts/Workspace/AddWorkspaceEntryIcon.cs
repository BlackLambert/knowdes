using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class AddWorkspaceEntryIcon : MonoBehaviour
    {
		[SerializeField]
		private Image _icon = null;
		[SerializeField]
		private TextMeshProUGUI _label = null;
		[SerializeField]
		private GameObject _iconObject = null;

        private Workspace _workspace;
		private AppColors _appColors;

        protected virtual void Start()
		{
            _workspace = FindObjectOfType<Workspace>();
			_appColors = FindObjectOfType<AppColors>();

			setColor(_appColors.Get(AppColors.Type.InteractionElementBackground));
			_iconObject.SetActive(_workspace.Count == 0);

			_workspace.OnPendingEntryChanged += onPendingChanged;
			_workspace.OnCountChanged += onCountChanged;
		}

		protected virtual void OnDestroy()
		{
			_workspace.OnPendingEntryChanged -= onPendingChanged;
			_workspace.OnCountChanged -= onCountChanged;
		}

		private void setColor(Color color)
		{
			_icon.color = color;
			_label.color = color;
		}

		private void onPendingChanged()
		{
			if(_workspace.PendingEntry != null)
				setColor(_appColors.Get(AppColors.Type.Highlighted));
			else
				setColor(_appColors.Get(AppColors.Type.InteractionElementBackground));
		}

		private void onCountChanged()
		{
			_iconObject.SetActive(_workspace.Count == 0);
		}
	}
}