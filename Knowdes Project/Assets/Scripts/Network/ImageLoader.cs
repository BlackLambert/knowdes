using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Knowdes
{
    public class ImageLoader : MonoBehaviour
    {
        private Dictionary<LoadRequest, Coroutine> _loadRoutines = new Dictionary<LoadRequest, Coroutine>();
        private Dictionary<LoadRequest, Action<Result>> _callbacks = new Dictionary<LoadRequest, Action<Result>>();


        protected virtual void OnDestroy()
		{
            StopAllCoroutines();
		}


        public void LoadImage(LoadRequest request, Action<Result> callback)
        {
            if (_loadRoutines.ContainsKey(request))
                throw new ArgumentException();
            _callbacks.Add(request, callback);
            Coroutine routine = StartCoroutine(loadPreviewImage(request));
            _loadRoutines.Add(request, routine);
        }

        public void CancelLoadingImage(LoadRequest request)
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

        //Source: https://stackoverflow.com/questions/31765518/how-to-load-an-image-from-url-with-unity
        private IEnumerator loadPreviewImage(LoadRequest request)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(request.Uri);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                _callbacks[request].Invoke(new Result(www.error));
            }
            else
            {
                Texture result = ((DownloadHandlerTexture)www.downloadHandler).texture;
                _callbacks[request].Invoke(new Result(result));
            }
            clean(request);
            www.Dispose();
        }

        public static ImageLoader New()
        {
            GameObject loaderObject = new GameObject(nameof(ImageLoader));
            return loaderObject.AddComponent<ImageLoader>();
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
            public bool RequestWasSuccessful => Texture != null && string.IsNullOrEmpty(Error);
            public Texture Texture { get; }
            public string Error { get; }


			public Result(string error)
			{
				Error = error;
                Texture = null;
            }

			public Result(Texture texture)
			{
                Error = string.Empty;
				Texture = texture;
			}
		}
    }
}