using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class PDFMetaDataExtractorButton : MonoBehaviour
    {

        private const string _noMetaDataErrorMessage = "Es konnten keine MetaDaten von diesem PDF extrahiert werden.";
        private const float _errorMessageDisplayTime = 3f;

        [SerializeField]
        private Button _button;
        [SerializeField]
        private PdfContentEditor _editor;
        private TemporaryNotificationDisplayer _notificationDisplayer;

        private string _path => _editor.Data.Path;
        private EntryData _entry => _editor.EntryData;

        protected virtual void Start()
		{
            _button.onClick.AddListener(onClick);
            _notificationDisplayer = FindObjectOfType<TemporaryNotificationDisplayer>();
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(onClick);
        }

        private void onClick()
        {
            if (string.IsNullOrEmpty(_path))
                return;
            extractMetaData();
        }

		private void extractMetaData()
		{
            PDFMetaDataExtractor extractor = new PDFMetaDataExtractor(_path);
            PDFMetaDataExtractor.Result result = extractor.Extract();
            if (result.IsEmpty)
                displayErrorNotification();
            else
                applyMetaDatas(result.GetExisting());
        }

		private void applyMetaDatas(List<MetaData> metaDatas)
		{
            foreach (MetaData data in metaDatas)
                applyMetaData(data);
		}

		private void applyMetaData(MetaData data)
		{
            if (_entry.Contains(data.Type))
                updateMetaData(_entry.Get(data.Type), data);
            else
                addMetaData(data);
		}

		private void updateMetaData(MetaData current, MetaData actual)
		{
            _entry.RemoveMetaData(current);
            addMetaData(actual);
		}

		private void addMetaData(MetaData data)
		{
            _entry.AddMetaData(data);
        }

		private void displayErrorNotification()
		{
            _notificationDisplayer.RequestDisplay(new TemporaryNotificationDisplayer.Request(_noMetaDataErrorMessage, _errorMessageDisplayTime));
		}
	}
}