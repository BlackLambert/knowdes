using TMPro;
using UnityEngine;

namespace Knowdes
{
	public class UnknownFileContent : Content<UnknownFileContentData>, FilebasedContent
	{

		[SerializeField]
		private TextMeshProUGUI _pathText;

		public string Path => Data.Path;

		FilebasedContentData FilebasedContent.Data => Data;

		protected override void onDataAdded(UnknownFileContentData data)
		{
			data.OnPathChanged += onPathChanged;
			updateView();
		}

		protected override void onDataRemoved(UnknownFileContentData data)
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