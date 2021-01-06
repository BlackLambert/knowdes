using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class ShowInPreviewToggle : MonoBehaviour
    {
		[SerializeField]
		private Toggle _toggle;
		[SerializeField]
		private MetaContentEditorShell _shell;

		protected virtual void Start()
		{
			_toggle.isOn = _shell.Data.ShowInPreview;
			_toggle.onValueChanged.AddListener(updateData);
		}

		protected virtual void OnDestroy()
		{
			_toggle.onValueChanged.RemoveListener(updateData);
		}

		private void updateData(bool isOn)
		{
			_shell.Data.ShowInPreview = isOn;
		}
	}
}