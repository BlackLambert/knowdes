using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Knowdes
{
    [CustomEditor(typeof(EmptyGraphic))]
    public class EmptyGraphicEditor : Editor
    {
		public override void OnInspectorGUI()
		{
		}
	}
}