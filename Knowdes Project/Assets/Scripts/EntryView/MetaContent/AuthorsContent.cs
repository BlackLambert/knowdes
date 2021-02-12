using TMPro;
using UnityEngine;

namespace Knowdes
{
    public class AuthorsContent : MetaContent<AuthorData>
    {
		[SerializeField]
		private TextMeshProUGUI _text;

		protected override void OnDestroy()
		{
			base.OnDestroy();
			onDataRemoved(Data);
		}

		private void initAuthor(Author author)
		{
			author.OnNameChanged += updateText;
			updateText();
		}

		private void cleanAuthor(Author author)
		{
			author.OnNameChanged -= updateText;
			updateText();
		}

		private void updateText()
		{
			string text = string.Empty;
			foreach (Author author in Data.AuthorsCopy)
			{
				if (!string.IsNullOrEmpty(text))
					text += " | ";
				text += author.Name;
			}
			_text.text = text;
		}

		protected override void onDataAdded(AuthorData data)
		{
			if (data == null)
				return;
			foreach (Author author in Data.AuthorsCopy)
				initAuthor(author);
			data.OnAuthorAdded += initAuthor;
			data.OnAuthorRemoved += cleanAuthor;
			updateText();
		}

		protected override void onDataRemoved(AuthorData data)
		{
			if (data == null)
				return;
			foreach (Author author in Data.AuthorsCopy)
				cleanAuthor(author);
			data.OnAuthorAdded -= initAuthor;
			data.OnAuthorRemoved -= cleanAuthor;
		}
	}
}