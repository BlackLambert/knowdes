using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ExtendToggleOnStart : MonoBehaviour
    {
        [SerializeField]
        private UIToggle _toggle;
        [SerializeField]
        private bool _showOnStart = true;

        protected virtual void Start()
		{
            _toggle.Toggle(_showOnStart);
        }
    }
}