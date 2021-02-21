using System;
using System.IO;
using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class FileNameDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private Content _content;
        private FilebasedContent _filebasedContent;

        protected virtual void Start()
		{
            if (!(_content is FilebasedContent))
                throw new ArgumentException();
            _filebasedContent = _content as FilebasedContent;
            updateText();
            _filebasedContent.Data.OnPathChanged += onPathChanged;
        }

        protected virtual void OnDestroy()
		{
            _filebasedContent.Data.OnPathChanged -= onPathChanged;
        }

		private void onPathChanged()
		{
            updateText();
        }

        private void updateText()
		{
            _text.text = Path.GetFileName(_filebasedContent.Data.Path);
        }
	}
}