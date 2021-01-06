using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class RecalculateLayoutOnExpand : MonoBehaviour
    {
        [SerializeField]
        private EntryVolume _content = null;

        protected virtual void Start()
		{
            _content.OnExpanded += recalculate;

        }

		protected virtual void OnDestroy()
		{
			_content.OnExpanded -= recalculate;
		}

		private void recalculate()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(_content.Base.parent.transform as RectTransform);
			Debug.Log("Recalculate");
		}

		protected virtual void Reset()
		{
			_content = GetComponent<EntryVolume>();
		}
	}
}