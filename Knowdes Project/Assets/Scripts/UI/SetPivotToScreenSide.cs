using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class SetPivotToScreenSide : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _target = null;

        protected virtual void Awake()
		{
            updatePivot();

        }

        protected virtual void Update()
		{
            updatePivot();

        }

        private void updatePivot()
		{
            float x = Input.mousePosition.x > Screen.width / 2 ? 1 : 0;
            float y = Input.mousePosition.y < Screen.height / 2 ? 0 : 1;
            _target.pivot = new Vector2(x, y);
        }

        protected virtual void Reset()
		{
            _target = GetComponent<RectTransform>();
		}
    }
}