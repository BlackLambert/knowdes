using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Knowdes
{
    public class FollowCursor : MonoBehaviour
    {
        [SerializeField]
        private Transform _target = null;

        protected virtual void Awake()
		{
            _target.position = Input.mousePosition; 
        }

        protected virtual void Update()
		{
            _target.position = Input.mousePosition;

        }
    }
}