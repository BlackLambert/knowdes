using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class PdfContent : Content<PdfContentData>, FilebasedContent
	{
		[SerializeField]
		private TextMeshProUGUI _pathText;

		public string Path => Data.Path;

		protected override void onDataAdded(PdfContentData data)
		{
			data.OnPathChanged += onPathChanged;
			updateView();
		}

		protected override void onDataRemoved(PdfContentData data)
		{
			data.OnPathChanged -= onPathChanged;
		}

		private void onPathChanged()
		{
			updateView();
		}

		private void updateView()
		{
			_pathText.text = Data.Path;
		}
	}
}