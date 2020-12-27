using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class ChangeSpriteOnSelect : MonoBehaviour
    {
        [SerializeField]
        private UISelectable _selectable = null;
        [SerializeField]
        private Sprite _unselectedSprite = null;
        [SerializeField]
        private Sprite _selectedSprite = null;
        [SerializeField]
        private Image _image = null;

        protected virtual void Start()
		{
            _selectable.OnSelected += updateImage;
            _selectable.OnDeselected += updateImage;
            updateImage();
        }

        protected virtual void OnDestroy()
		{
            _selectable.OnSelected -= updateImage;
            _selectable.OnDeselected -= updateImage;
        }

        protected virtual void Reset()
		{
            _image = GetComponent<Image>();
            _selectable = GetComponent<UISelectable>();
		}

        private void updateImage()
		{
            _image.sprite = _selectable.Selected ? _selectedSprite : _unselectedSprite;

        }
    }
}