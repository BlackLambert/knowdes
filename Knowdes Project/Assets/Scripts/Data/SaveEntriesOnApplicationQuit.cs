using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class SaveEntriesOnApplicationQuit : MonoBehaviour
    {
		[SerializeField]
		private EntryDataRepository _entryDataRepository;


		private void OnApplicationQuit()
		{
			_entryDataRepository.SaveAll();
		}
	}
}