using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class EntryContent : MonoBehaviour
    {
        [SerializeField]
        private Transform _base;
        public Transform Base => _base;
    }
}