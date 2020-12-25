using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class Selector : MonoBehaviour
    {
        public UISelectable Current { get; private set; }

        public void Next()
		{
            UISelectable current = Current;
            Deselect(Current);
            Select(current.Next);
		}

        public void Select(UISelectable selectable)
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

        public void Deselect(UISelectable selectable)
		{
            if (Current == null)
                throw new ArgumentNullException();
            if (Current != selectable)
                throw new ArgumentException();
            UISelectable current = Current;
            Current = null;
            current.Deselect();
        }
    }
}