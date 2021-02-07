using B83.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowdes
{
    public class FileDropReceiver : MonoBehaviour
    {
        private TemporaryNotificationDisplayer _notificationDisplayer;

        void Start()
        {
            UnityDragAndDropHook.InstallHook();
            UnityDragAndDropHook.OnDroppedFiles += OnFilesDropped;
            _notificationDisplayer = FindObjectOfType<TemporaryNotificationDisplayer>();
        }

		void OnDestroy()
        {
            UnityDragAndDropHook.UninstallHook();
        }

        private void OnFilesDropped(List<string> aPathNames, POINT aDropPoint)
        {
            string first = aPathNames.FirstOrDefault();
            TemporaryNotificationDisplayer.Request request = new TemporaryNotificationDisplayer.Request(
                $"File dropped from {first} at {aDropPoint}");
            _notificationDisplayer.RequestDisplay(request);
        }
    }
}