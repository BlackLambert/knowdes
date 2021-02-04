using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    [ExecuteInEditMode]
    public class ConstantPhysicalSizeCanvasScaler : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas = null;
        [SerializeField]
        private float _targetDPI = 96;

        protected virtual void Update()
		{
            float screenDPI = Screen.dpi;
            _canvas.scaleFactor = screenDPI / _targetDPI;
        }
    }
}