using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class ShowOnDestroyable : MonoBehaviour
    {
        [SerializeField]
        private MetaContentEditorShell _editor;
        [SerializeField]
        private GameObject _target;

        protected virtual void Start()
		{
            _target.SetActive(_editor.Data.Destroyable);
        }
    }
}