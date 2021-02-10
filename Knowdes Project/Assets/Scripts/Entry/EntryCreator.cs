using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class EntryCreator : MonoBehaviour
    {
        private EntryFactory _factory;
        private ContextPanel _contextPanel;
        private EntriesList _entryList;
        private EntryDataFactory _entryDatasFactory;
        private EntryDataRepository _entryRepository;
        private ContentDataFactory _contentDataFactory;



        protected virtual void Start()
        {
            _factory = FindObjectOfType<EntryFactory>();
            _contextPanel = FindObjectOfType<ContextPanel>();
            _entryDatasFactory = new EntryDataFactory();
            _contentDataFactory = new ContentDataFactory();
            _entryList = FindObjectOfType<EntriesList>();
            _entryRepository = FindObjectOfType<EntryDataRepository>();
        }

        public void Create(ContentData data)
		{
            EntryData entryData = _entryDatasFactory.Create(data);
            _entryRepository.Add(entryData);
            Entry newEntry = _factory.Create(entryData);
            _entryList.Add(newEntry);
            _contextPanel.ShowEditPanel(newEntry);
        }

        public void Create(ContentType type)
		{
            ContentData content = _contentDataFactory.Create(type);
            Create(content);
        }
    }
}