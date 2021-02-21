using B83.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Knowdes
{
    public class FileDropReceiver : MonoBehaviour
    {
        private const float _errorNotificationDisplayTime = 3;
        public event Action<Uri, Vector2> OnFileDropped;

        private FilePathConverter _uriConverter;
        private TemporaryNotificationDisplayer _notificationDisplayer;

        void Start()
        {
            UnityDragAndDropHook.InstallHook();
            UnityDragAndDropHook.OnDroppedFiles += OnFilesDropped;

            _notificationDisplayer = FindObjectOfType<TemporaryNotificationDisplayer>();
            _uriConverter = new FilePathConverter();
        }

		void OnDestroy()
        {
            UnityDragAndDropHook.UninstallHook();
        }

        private void OnFilesDropped(List<string> pathNames, POINT dropPoint)
        {
            Vector2 point = new Vector2(dropPoint.x, dropPoint.y);
            foreach(string path in pathNames)
			{
                UriConverter.Result result = _uriConverter.Convert(path);
                if (result.Successful)
                    OnFileDropped(result.Uri, point);
                else
                    displayErrorNotification(result.Error);
            }
        }

		private void displayErrorNotification(string error)
		{
            TemporaryNotificationDisplayer.Request request = new TemporaryNotificationDisplayer.Request(error, _errorNotificationDisplayTime);
            _notificationDisplayer.RequestDisplay(request);
        }
	}
}