using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowdes
{
    public class SeparatorController : MonoBehaviour
    {
        [SerializeField]
        private List<Panel> _panels = null;
        [SerializeField]
        private AdjustableSeparator _separator = null;
        [SerializeField]
        private int _index = 0;
        public int Index
		{
			set
			{
                _index = value;
                setPanels();
			}
		}

        protected virtual void Start()
		{
            setPanels();
            foreach (Panel panel in _panels)
                panel.OnCollapsedChanged += onCollapseChanged;
		}

		protected virtual void OnDestroy()
		{
            foreach (Panel panel in _panels)
                panel.OnCollapsedChanged -= onCollapseChanged;
        }

        protected virtual void Reset()
		{
            _separator = GetComponent<AdjustableSeparator>();
            _panels = GetComponentsInParent<Panel>().ToList();
        }

		private void setPanels()
		{
            int index = _index;
            Panel first = null;
            Panel second = null;

            for(int i = index - 1; i>= 0; i--)
			{
                if (_panels[i].Collapsed)
                    continue;
                first = _panels[i];
                break;
            }

            for (int i = index; i < _panels.Count; i++)
            {
                if (_panels[i].Collapsed)
                    continue;
                second = _panels[i];
                break;
            }

            _separator.Base.gameObject.SetActive(first != null && second != null);
            _separator.First = first;
            _separator.Second = second;
            activateSeparators();
        }

        private void activateSeparators()
		{
            int count = _panels.Where(p => !p.Collapsed).Count();
            int index = _index;
            bool activate = _separator.First != null && _separator.Second != null && !_panels[index].Collapsed;
            _separator.Base.gameObject.SetActive(activate);
		}

        private void onCollapseChanged(Panel _)
        {
            setPanels();
        }
    }
}