using System;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class OpenFileContentButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Content _content;

        private FilebasedContent _filebasedContent;

        protected virtual void Start()
		{
            if (!(_content is FilebasedContent))
                throw new ArgumentException();
            _filebasedContent = (FilebasedContent)_content;
            _button.onClick.AddListener(onClick);

        }

        protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(onClick);
        }

		private void onClick()
		{
            Application.OpenURL(_filebasedContent.Path);
		}
	}
}