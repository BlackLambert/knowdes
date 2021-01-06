using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Knowdes.Prototype
{
    public class ContentTypeDropdown : MonoBehaviour
    {
        [SerializeField]
        private TMP_Dropdown _dropdown = null;

        private EditPanel _editPanel;
		private Entry _current = null;
		private ContentDataFactory _contentDataFactory;

		private List<ContentDataType> _typesList = new List<ContentDataType>();

		private ContentData Content => _editPanel.Entry.Volume.Data.Content;

		protected virtual void Start()
		{
			_editPanel = FindObjectOfType<EditPanel>();
			_contentDataFactory = new ContentDataFactory();
			initDropdown();
			initContent();
			_editPanel.OnEntryChanged += onEntryChanged;
		}

		protected virtual void OnDestroy()
		{
			_dropdown.onValueChanged.RemoveListener(onValueChanged);
			if(_editPanel != null)
				_editPanel.OnEntryChanged -= onEntryChanged;

		}

		private void initDropdown()
		{
			_dropdown.options.Clear();
			List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
			foreach (ContentDataType value in Enum.GetValues(typeof(ContentDataType)))
			{
				_typesList.Add(value);
				options.Add(new TMP_Dropdown.OptionData(value.GetName()));
			}
			_dropdown.AddOptions(options);
		}

		private void initContent()
		{
			_current = _editPanel.Entry;
			if (_current == null)
				return;
			_dropdown.value = Content == null ? 0 : _typesList.IndexOf(Content.Type);

		}



		private void onValueChanged(int value)
		{
			ContentDataType type = _typesList[value];
			_current.Volume.Data.Content = type != ContentDataType.Unset ? _contentDataFactory.Create(_typesList[value]) : null;
		}



		private void onEntryChanged()
		{
			_dropdown.onValueChanged.RemoveListener(onValueChanged);
			initContent();
			_dropdown.onValueChanged.AddListener(onValueChanged);
		}
	}
}