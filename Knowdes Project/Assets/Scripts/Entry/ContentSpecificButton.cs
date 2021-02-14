using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ContentSpecificButton : MonoBehaviour
    {
        [SerializeField]
        private Transform _base;
        public Transform Base => _base;
    }
}