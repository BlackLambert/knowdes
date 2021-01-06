using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class UISelectable : MonoBehaviour, IPointerClickHandler, SelectorSelectable
    {
		private Selector _selector;

		[SerializeField]
		private UISelectable _next = null;
		public UISelectable Next => _next;

        public event Action OnSelected;
        public event Action OnDeselected;

		public bool Selected { get; private set; } = false;
		public bool Blocked { get; set; } = false;

		protected virtual void Awake()
		{
			_selector = FindObjectOfType<Selector>();
		}

		protected virtual void OnDestroy()
		{
			if (Selected && _selector != null)
				_selector.Deselect(this);
		}

		public void OnPointerClick(PointerEventData eventData)
		{

			//Debug.Log($"{Selected} && {!Blocked}");
			if (Blocked)
				return;
			if (Selected)
				_selector.Deselect(this);
			else
				_selector.Select(this);

			//Debug.Log($"{Selected} && {!Blocked}");
		}

		public void TrySelect()
		{
			if (Blocked || Selected)
				return;
			_selector.Select(this);
		}

		void SelectorSelectable.Select()
		{
			if (Selected)
				throw new InvalidOperationException();
			Selected = true;
			OnSelected?.Invoke();
		}

		void SelectorSelectable.Deselect()
		{
			if (!Selected)
				throw new InvalidOperationException();
			Selected = false;
			OnDeselected?.Invoke();
		}
	}
}