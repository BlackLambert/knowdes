using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class EntryFactory : MonoBehaviour
    {
        [SerializeField]
        private Entry _prefab;

        public Entry Create(EntryData data)
		{
            Entry result = Instantiate(_prefab);
            result.Volume.Data = data;
            result.Selectable.TrySelect();
            return result;
        }
	}
}