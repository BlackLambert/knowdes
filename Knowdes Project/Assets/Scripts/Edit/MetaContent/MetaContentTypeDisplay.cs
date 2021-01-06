using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class MetaContentTypeDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
		[SerializeField]
		private MetaContentEditorShell _shell;

		protected void Start()
		{
			_text.text = _shell.Data.Type.GetName();
		}
	}
}