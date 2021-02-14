using System;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class ContentSpecificButtonsToActionPanelAdder : MonoBehaviour
    {
        [SerializeField]
        private VolumeContainer _volumeContainer;
        [SerializeField]
        private Transform _hook;

        private List<ContentSpecificButton> _buttons = new List<ContentSpecificButton>();

        protected virtual void Start()
		{
            if (_volumeContainer.Volume == null)
                _volumeContainer.OnVolumeSet += onVolumeSet;
            else if (_volumeContainer.Volume.Content == null)
                _volumeContainer.Volume.OnContentChanged += onContentSet;
            else
                addButtons();
        }

		private void onContentSet()
		{
            _volumeContainer.Volume.OnContentChanged -= onContentSet;
            addButtons();
        }

		private void onVolumeSet()
		{
            _volumeContainer.OnVolumeSet -= onVolumeSet;
            addButtons();
        }

        private void addButtons()
		{
            addButtons(_volumeContainer.Volume.Content.ContentSpecificButtons);
        }

		private void addButtons(IEnumerable<ContentSpecificButton> buttons)
		{
            foreach (ContentSpecificButton button in buttons)
                addButton(button);
        }

        private void addButton(ContentSpecificButton button)
		{
            _buttons.Add(button);
            button.Base.SetParent(_hook, false);
        }
    }
}