using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    [ExecuteInEditMode]
    public class ConstantPhysicalSizeCanvasScaler : MonoBehaviour
    {
        private const float _targetDPI = 120;

        [SerializeField]
        private Canvas _canvas = null;

        protected virtual void Update()
		{
            float screenDPI = Screen.dpi;
            _canvas.scaleFactor = screenDPI / _targetDPI;
        }
    }
}