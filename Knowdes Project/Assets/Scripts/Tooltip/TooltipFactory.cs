using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class TooltipFactory : MonoBehaviour
    {
        [SerializeField]
        private Tooltip _prefab = null;

        public Tooltip Create(string text)
		{
            Tooltip result = Instantiate(_prefab);
            result.SetText(text);
            return result;
		}
    }
}