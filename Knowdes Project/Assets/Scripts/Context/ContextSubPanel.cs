using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public abstract class ContextSubPanel : MonoBehaviour
    {
        public abstract ContextSubPanelType Type { get; }

        [SerializeField]
        private RectTransform _base;

        public void Show(bool show)
		{
            _base.gameObject.SetActive(show);
        }
    }
}