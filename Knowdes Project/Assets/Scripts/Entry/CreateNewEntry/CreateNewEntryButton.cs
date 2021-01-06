
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
        private EntryFactory _factory;
        private ContextPanel _contextPanel;

        [SerializeField]
        private ContentDataType _type;

        protected virtual void Start()
		{
            _button.onClick.AddListener(createEntry);
            _label.text = _type.GetName();
            _factory = FindObjectOfType<EntryFactory>();
            _contextPanel = FindObjectOfType<ContextPanel>();
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(createEntry);
        }

        private void createEntry()
        {
            Entry newEntry = _factory.CreateNew(_type);
            _contextPanel.ShowEditPanel(newEntry);
        }
    }
}