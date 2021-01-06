using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knowdes
{
    public class ShowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject _target = null;
		[SerializeField]
		private bool _show = true;

		protected virtual void Start()
		{
			_target.SetActive(!_show);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_target.SetActive(_show);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_target.SetActive(!_show);
		}
	}
}