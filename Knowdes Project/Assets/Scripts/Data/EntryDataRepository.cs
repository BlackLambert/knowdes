using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class EntryDataRepository : MonoBehaviour
    {
        DataManager _dataManager;
        private Dictionary<Guid, EntryData> _iDToDatas = new Dictionary<Guid, EntryData>();
		public IEnumerable<EntryData> Datas => _iDToDatas.Values;

		protected virtual void Awake()
		{
            _dataManager = new DataManager();
            loadDatas();
        }

        protected virtual void OnDestroy()
		{
			_dataManager.Dispose();
		}

		private void loadDatas()
		{
            List<EntryData> datas = _dataManager.GetAllEntrys();
            _iDToDatas = createDataDictionary(datas);
		}

		private Dictionary<Guid, EntryData> createDataDictionary(List<EntryData> datas)
		{
            Dictionary<Guid, EntryData> result = new Dictionary<Guid, EntryData>();
            foreach (EntryData data in datas)
                result.Add(data.Id, data);
            return result;
        }

       

		public void Save(EntryData data)
		{
			_dataManager.AddOrReplaceEntryData(data);
            //Debug.Log($"Saved {data.ToString()}");
		}

		internal void SaveAll()
		{
			foreach (EntryData data in Datas)
				Save(data);
		}

		public EntryData GetBy(Guid iD)
		{
            if (!_iDToDatas.ContainsKey(iD))
                throw new ArgumentException();
            return _iDToDatas[iD];
		}

        public void Add(EntryData data)
		{
			if (_iDToDatas.ContainsKey(data.Id))
				throw new ArgumentException();
			_iDToDatas.Add(data.Id, data);
			Save(data);
		}

		public void Delete(EntryData data)
		{
            if (!_iDToDatas.ContainsKey(data.Id))
                throw new ArgumentException();
            _iDToDatas.Remove(data.Id);
            _dataManager.DeleteEntry(data);
            data.Dispose();
        }
    }
}