
using UnityEngine;

namespace Knowdes.Prototype
{
    public class OnFirstEntryShower : MonoBehaviour
    {
        [SerializeField]
        private GameObject _objectToHide = null;
        [SerializeField]
        private EntriesList _entries = null;
        [SerializeField]
        private bool _show = false;

        protected virtual void Start()
		{
            _entries.OnCountChanged += onCountChanged;
            onCountChanged();
        }

		protected virtual void OnDestroy()
		{
            _entries.OnCountChanged -= onCountChanged;
        }

        private void onCountChanged()
        {
            bool show = _entries.Count == 0 && !_show || _entries.Count > 0 && _show;
            _objectToHide.SetActive(show);
        }
    }
}