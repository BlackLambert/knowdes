using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Knowdes
{
    public class LinkPreviewLoader : MonoBehaviour
    {
        private const string _requestURL = "https://api.linkpreview.net/?key={0}&fields=title,description,image&q={1}";
        private const string _apiKey = "354ff6dd42114a7f5c4cbef9a780109c";

        private Dictionary<LoadRequest, Coroutine> _loadRoutines = new Dictionary<LoadRequest, Coroutine>();
        private Dictionary<LoadRequest, Action<Result>> _callbacks = new Dictionary<LoadRequest, Action<Result>>();


        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
        }


        public void LoadPreview(LoadRequest request, Action<Result> callback)
        {
            if (_loadRoutines.ContainsKey(request))
                throw new ArgumentException();
            _callbacks.Add(request, callback);
            Coroutine routine = StartCoroutine(loadPreviewData(request));
            _loadRoutines.Add(request, routine);
        }

        public void StopLoadingPreview(LoadRequest request)
        {
            if (!_loadRoutines.ContainsKey(request))
                throw new ArgumentException();
            StopCoroutine(_loadRoutines[request]);
            clean(request);
        }

        private void clean(LoadRequest request)
        {
            _loadRoutines.Remove(request);
            _callbacks.Remove(request);
        }

        private IEnumerator loadPreviewData(LoadRequest request)
        {
            UnityWebRequest www = UnityWebRequest.Get(string.Format(_requestURL, _apiKey, request.Uri));
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                _callbacks[request].Invoke(new Result(www.error));
            }
            else
            {
                string json = www.downloadHandler.text;
                LinkPreviewData data = JsonUtility.FromJson<LinkPreviewData>(json);
                _callbacks[request].Invoke(new Result(data));
            }
            www.Dispose();
        }

        public static LinkPreviewLoader New()
        {
            GameObject loaderObject = new GameObject(nameof(LinkPreviewLoader));
            return loaderObject.AddComponent<LinkPreviewLoader>();
        }

        public void Destroy()
		{
            Destroy(gameObject);
		}

        public class LoadRequest
        {
            public Uri Uri { get; }

            public LoadRequest(Uri uri)
            {
                Uri = uri;
            }
        }

        public class Result
        {
            public bool RequestWasSuccessful => Data != null && string.IsNullOrEmpty(Error);
            public LinkPreviewData Data { get; }
            public string Error { get; }


            public Result(string error)
            {
                Error = error;
                Data = null;
            }

            public Result(LinkPreviewData data)
            {
                Error = string.Empty;
                Data = data;
            }
        }
    }
}