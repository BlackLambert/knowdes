using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ShowOnStart : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target = null;
        [SerializeField]
        private bool _show = false;

        protected virtual void Start()
        {
            _target.SetActive(_show);
        }
    }
}