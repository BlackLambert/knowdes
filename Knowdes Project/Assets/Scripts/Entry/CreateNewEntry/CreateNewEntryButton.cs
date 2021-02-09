
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
        private EntriesList _entryList;
        private EntryDataFactory _entryDatasFactory;
        private EntryDataRepository _entryRepository;
        private ContentDataFactory _contentDataFactory;

        [SerializeField]
        private ContentType _type;

        protected virtual void Start()
		{
            _button.onClick.AddListener(createEntry);
            _label.text = _type.GetName();
            _factory = FindObjectOfType<EntryFactory>();
            _contextPanel = FindObjectOfType<ContextPanel>();
            _entryDatasFactory = new EntryDataFactory();
            _contentDataFactory = new ContentDataFactory();
            _entryList = FindObjectOfType<EntriesList>();
            _entryRepository = FindObjectOfType<EntryDataRepository>();
        }

		protected virtual void OnDestroy()
		{
            _button.onClick.RemoveListener(createEntry);
        }

        private void createEntry()
        {
            ContentData content = _contentDataFactory.Create(_type);
            EntryData entryData = _entryDatasFactory.Create(content);
            _entryRepository.Add(entryData);
            Entry newEntry = _factory.Create(entryData);
            _entryList.Add(newEntry);
            _contextPanel.ShowEditPanel(newEntry);
        }
    }
}