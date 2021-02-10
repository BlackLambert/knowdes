
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class CreateNewEntryButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private TextMeshProUGUI _label;
        private EntryCreator _entryCreator;

        [SerializeField]
        private ContentType _type;

        protected virtual void Start()
		{
            _button.onClick.AddListener(createEntry);
            _label.text = _type.GetName();
            _entryCreator = FindObjectOfType<EntryCreator>();
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(createEntry);
        }

        private void createEntry()
        {
            _entryCreator.Create(_type);
        }
    }
}