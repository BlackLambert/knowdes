using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class CoroutineHelper : MonoBehaviour
    {
        public static CoroutineHelper New()
		{
            GameObject helperObject = new GameObject("CoroutineHelper");
            return helperObject.AddComponent<CoroutineHelper>();
        }
    }
}