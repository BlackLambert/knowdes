using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class ContextPanel : MonoBehaviour
    {
        [SerializeField]
        private EditPanel _editPanel;
        [SerializeField]
        private CreateNewEntryPanel _createNewEntryPanel;
        [SerializeField]
        private Panel _panel;
        [SerializeField]
        private ContextSubPanelType _defaultPanel = ContextSubPanelType.NewEntryCreation;


        protected virtual void Start()
		{
            showSubPanel(_defaultPanel);
        }

        public void ShowEditPanel(Entry targetEntry)
		{
            _editPanel.InitWith(targetEntry);
            showSubPanel(ContextSubPanelType.Edit);
            showContextPanel();
        }

        public void ShowCreateNewEntryPanel()
        {
            showSubPanel(ContextSubPanelType.NewEntryCreation);
            showContextPanel();
        }

        private void showContextPanel()
		{
            if (!_panel.Collapsed)
                return;
            _panel.Expand();
		}

        public void Hide()
        {
            if (_panel.Collapsed)
                return;
            _panel.Collapse();
        }

        private void showSubPanel(ContextSubPanelType type)
		{
            _createNewEntryPanel.Show(type == _createNewEntryPanel.Type);
            _editPanel.Show(type == _editPanel.Type);
        }
    }
}