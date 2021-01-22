using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Knowdes
{
    public class LinkPreview : MonoBehaviour
    {
        private const string _requestURL = "https://api.linkpreview.net/?key={0}&fields=title,description,image&q={1}";
        private const string _apiKey = "354ff6dd42114a7f5c4cbef9a780109c";

        [SerializeField]
        private TextMeshProUGUI _titleText;
        [SerializeField]
        private TextMeshProUGUI _descriptionText;
        [SerializeField]
        private RawImage _previewImage;
        [SerializeField]
        private AspectRatioFitter _imageFitter;
        [SerializeField]
        private GameObject _container;

        private Uri _uri = null;
        public Uri URL
		{
            set
			{
                _uri = value;
                updatePreview();
            }
		}

        private Coroutine _fetchPreviewRoutine;
        private Coroutine _fetchPreviewImageRoutine;
        private LinkPreviewData _data = null;

        private CoroutineHelper _coroutineHelper;

        protected virtual void Awake()
        {
            GameObject helperObject = new GameObject("CoroutineHelper");
            _coroutineHelper = helperObject.AddComponent<CoroutineHelper>();
        }

        protected virtual void Start()
        {
            updatePreview();
        }

        protected virtual void OnDestroy()
		{
            if(_coroutineHelper != null)
                Destroy(_coroutineHelper.gameObject);
		}

        private void updatePreview()
		{
            clean();
            if (_fetchPreviewRoutine != null)
                _coroutineHelper.StopCoroutine(_fetchPreviewRoutine);
            if(_uri != null)
                _fetchPreviewRoutine = _coroutineHelper.StartCoroutine(GetPreviewData());
		}

        private void updateVisablity()
		{
            _container.SetActive(_data != null);
		}

        private void clean()
		{
            _container.SetActive(false);
            _data = null;
            _previewImage.texture = null;
        }

        private IEnumerator GetPreviewData()
		{
            UnityWebRequest www = UnityWebRequest.Get(string.Format(_requestURL, _apiKey, _uri));
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                _data = JsonUtility.FromJson<LinkPreviewData>(json);
                updateVisablity();
                updateView();
            }
            www.Dispose();
        }

        private void updateView()
		{
            if (_data == null)
                return;
            if (_fetchPreviewImageRoutine != null)
                _coroutineHelper.StopCoroutine(_fetchPreviewImageRoutine);
            _titleText.text = _data.title;
            _descriptionText.text = _data.description;
            if (!string.IsNullOrEmpty(_data.image))
                _fetchPreviewImageRoutine = _coroutineHelper.StartCoroutine(loadPreviewImage());
            else
                _previewImage.texture = null;
        }

        //Source: https://stackoverflow.com/questions/31765518/how-to-load-an-image-from-url-with-unity
        private IEnumerator loadPreviewImage()
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(_data.image);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
			{
                _previewImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                _previewImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _previewImage.texture.width);
                _previewImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _previewImage.texture.height);
                float ratio = (float)_previewImage.texture.width / (float)_previewImage.texture.height;
                _imageFitter.aspectRatio = ratio;
            }
            www.Dispose();
        }


        private class LinkPreviewData
		{
            public string title;
            public string description;
            public string image;
		}
    }
}