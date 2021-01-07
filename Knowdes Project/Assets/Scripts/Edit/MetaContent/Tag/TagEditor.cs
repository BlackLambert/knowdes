using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Knowdes.Prototype
{
	public class TagEditor : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _input;
		[SerializeField]
		private RectTransform _base;
		public RectTransform Base => _base;

        private Tag _tag;
        public Tag Tag
		{
            get => _tag;
			set
			{
                _tag = value;
				initView();
			}
		}

		public TagsData Data { get; set; }

        protected virtual void Start()
		{
            _input.onValueChanged.AddListener(updateTagName);
		}

		protected virtual void OnDestroy()
		{
			_input.onValueChanged.RemoveListener(updateTagName);
		}

		private void updateTagName(string arg0)
		{
			_tag.Name = _input.text;
		}

		private void initView()
		{
			_input.text = _tag.Name;
		}
	}
}