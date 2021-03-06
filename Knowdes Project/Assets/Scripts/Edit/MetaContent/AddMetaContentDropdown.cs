﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class AddMetaContentDropdown : MonoBehaviour
    {
		[SerializeField]
		private TMP_Dropdown _dropdown = null;

		private EditPanel _editPanel;
		private Entry _current = null;
		private MetaDataFactory _dataFactory;

		private List<MetaDataType> _selectableTypesList = new List<MetaDataType>();


		protected virtual void Start()
		{
			_editPanel = FindObjectOfType<EditPanel>();
			_dataFactory = new MetaDataFactory ();
			_current = _editPanel.Entry;
			onEntryChanged();
			_editPanel.OnEntryChanged += onEntryChanged;
		}

		protected virtual void OnDestroy()
		{
			_dropdown.onValueChanged.RemoveListener(onValueChanged);
			if (_editPanel != null)
				_editPanel.OnEntryChanged -= onEntryChanged;
		}

		private void updateDropdown()
		{
			if (_current == null)
				return;

			_dropdown.options.Clear();
			_selectableTypesList.Clear();
			List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
			foreach (MetaDataType value in Enum.GetValues(typeof(MetaDataType)))
			{
				if (_current.Data.Contains(value))
					continue;
				_selectableTypesList.Add(value);
				options.Add(new TMP_Dropdown.OptionData(value.GetName()));
			}
			_dropdown.AddOptions(options);
			_dropdown.value = 0;
		}



		private void onValueChanged(int value)
		{
			if (value == 0)
				return;
			_current.Data.AddMetaData(_dataFactory.CreateNew(_selectableTypesList[value]));
		}



		private void onEntryChanged()
		{
			_dropdown.onValueChanged.RemoveListener(onValueChanged);
			removeEntryListeners();
			_current = _editPanel.Entry;
			updateDropdown();
			addEntryListeners();
			_dropdown.onValueChanged.AddListener(onValueChanged);
		}

		private void addEntryListeners()
		{
			if (_current == null)
				return;
			_current.Data.OnContentChanged += onContentChanged;
			_current.Data.OnMetaDataAdded += onMetaDataChanged;
			_current.Data.OnMetaDataRemoved += onMetaDataChanged;
		}

		private void removeEntryListeners()
		{
			if (_current == null)
				return;
			_current.Data.OnContentChanged -= onContentChanged;
			_current.Data.OnMetaDataAdded -= onMetaDataChanged;
			_current.Data.OnMetaDataRemoved -= onMetaDataChanged;
		}

		private void onMetaDataChanged(MetaData obj)
		{
			updateDropdown();
		}

		private void onContentChanged()
		{
			updateDropdown();
		}
	}
}