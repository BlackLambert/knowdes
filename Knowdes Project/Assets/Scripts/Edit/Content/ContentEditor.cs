using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ContentEditor : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;
    }
}