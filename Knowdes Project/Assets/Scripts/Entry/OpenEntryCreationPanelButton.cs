using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class OpenEntryCreationPanelButton : MonoBehaviour
    {
		[SerializeField]
		private Button _addButton = null;
		private ContextPanel _contextPanel = null;

		protected virtual void Start()
		{
			_addButton.onClick.AddListener(onClick);
			_contextPanel = FindObjectOfType<ContextPanel>();
		}

		protected virtual void OnDestroy()
		{
			_addButton.onClick.RemoveListener(onClick);
		}

		protected virtual void Reset()
		{
			_addButton = GetComponent<Button>();
		}

		private void onClick()
		{
			_contextPanel.ShowCreateNewEntryPanel();
		}
	}
}
