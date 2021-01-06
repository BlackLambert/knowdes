using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class Selector : MonoBehaviour
    {
        public SelectorSelectable Current { get; private set; }

        public void Select(SelectorSelectable selectable)
		{
            if (Current == selectable)
                throw new InvalidOperationException();
            if (selectable == null)
                throw new ArgumentNullException();
            if (Current != null)
                Deselect(Current);
            Current = selectable;
            Current.Select();
		}

        public void Deselect(SelectorSelectable selectable)
		{
            if (Current == null)
                throw new ArgumentNullException();
            if (Current != selectable)
                throw new ArgumentException();
            SelectorSelectable current = Current;
            Current = null;
            current.Deselect();
        }

        public void DeselectAll()
		{
            if (Current == null)
                return;
            Deselect(Current);
        }
    }
}