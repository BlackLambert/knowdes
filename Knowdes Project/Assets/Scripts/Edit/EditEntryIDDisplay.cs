﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EditEntryIDDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private EditPanel _editPanel;

        protected virtual void Start()
		{
            _editPanel = FindObjectOfType<EditPanel>();
            updateText();
            _editPanel.OnEntryChanged += updateText;

        }

        protected virtual void OnDestroy()
		{

            _editPanel.OnEntryChanged -= updateText;
        }

        private void updateText()
		{
            _text.text =$"#{ (_editPanel.Entry != null ? _editPanel.Entry.Data.Id.ToString() : string.Empty)}";

        }
    }
}