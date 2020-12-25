
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class ColorSetterOnStart : MonoBehaviour
    {
        private AppColors _colors;

        [SerializeField]
        private Graphic _target = null;
        [SerializeField]
        private AppColors.Type _type;

        protected virtual void Start()
		{
            _colors = FindObjectOfType<AppColors>();
            _target.color = _colors.Get(_type);
        }

        protected virtual void Reset()
        {
            _target = GetComponent<Graphic>();
        }
    }
}