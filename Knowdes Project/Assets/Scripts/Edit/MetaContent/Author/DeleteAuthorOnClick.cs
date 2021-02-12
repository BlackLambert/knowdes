using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class DeleteAuthorOnClick : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
		[SerializeField]
		private AuthorEditor _authorEditor;

        protected virtual void Start()
		{
			_button.onClick.AddListener(deleteAuthor);

        }

		protected virtual void OnDestroy()
		{
			_button.onClick.RemoveListener(deleteAuthor);
		}

		private void deleteAuthor()
		{
			_authorEditor.DeleteAuthor();
		}
	}
}