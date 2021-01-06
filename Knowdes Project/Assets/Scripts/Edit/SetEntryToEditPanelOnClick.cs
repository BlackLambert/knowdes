
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes.Prototype
{
    public class SetEntryToEditPanelOnClick : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Entry _entry;

        private EditPanel _editPanel;

        protected virtual void Start()
        {
            _editPanel = FindObjectOfType<EditPanel>();
            _button.onClick.AddListener(onClick);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(onClick);
        }

        private void onClick()
        {
            _editPanel.SetEntry(_entry);
        }
    }
}