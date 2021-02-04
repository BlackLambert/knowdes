using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class TemporaryNotificationDisplayer : MonoBehaviour
    {
		[SerializeField]
		private TemporaryNotification _notificationPrefab = null;

		private Queue<Request> _pendungRequests = new Queue<Request>();
		private TemporaryNotification _currentNotification = null;
		private bool _readyToDisplayNext => _currentNotification == null && _pendungRequests.Count > 0;


		public void Show(Request request)
		{
			enqueue(request);
			checkDisplayNext();
		}

		private void checkDisplayNext()
		{
			if (_readyToDisplayNext)
				displayNext();
		}

		private void displayNext()
		{
			Request nextRequest = _pendungRequests.Dequeue();
			createNextNotification(nextRequest);
		}

		private void onNotificationHidden()
		{
			destroyCurrentNotification();
			checkDisplayNext();
		}
		private void createNextNotification(Request nextRequest)
		{
			_currentNotification = Instantiate(_notificationPrefab);
			_currentNotification.OnHidden += onNotificationHidden;
			_currentNotification.Display(nextRequest.Message, nextRequest.DisplayTime);
		}

		private void destroyCurrentNotification()
		{
			_currentNotification.OnHidden -= onNotificationHidden;
			_currentNotification.Destroy();
			_currentNotification = null;
		}

		private void enqueue(Request request)
		{
			_pendungRequests.Enqueue(request);
		}

		public class Request
		{
            private const float _defaultDisplayTime = 2f;

			public string Message { get; }
			public float DisplayTime { get; }

			public Request(string message, float displayTime)
			{
				Message = message;
				DisplayTime = displayTime;
			}

			public Request(string message)
			{
				Message = message;
				DisplayTime = _defaultDisplayTime;
			}
		}
    }
}