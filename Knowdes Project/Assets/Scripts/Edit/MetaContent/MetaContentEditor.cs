using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public abstract class MetaContentEditor : MonoBehaviour
    {

        [SerializeField]
        private RectTransform _base;
        public RectTransform Base => _base;

        public abstract MetaData MetaData { get; }
    }
}