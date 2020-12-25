using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    [CreateAssetMenu(fileName = "AppColorSettings", menuName = "Knowdes/AppColorsSettings", order = 1)]
    public class AppColorsSettings : ScriptableObject
    {
        [SerializeField]
        private Color _selected = Color.white;
        public Color Selected => _selected;

        [SerializeField]
        private Color _highlighted = new Color(0.85f, 0.85f, 0.85f, 1f);
        public Color Highlighted => _highlighted;

        [SerializeField]
        private Color _interactable = new Color(0.6f, 0.6f, 0.6f, 1f);
        public Color Interactable => _interactable;

        [SerializeField]
        private Color _mainBackground = new Color(0.3f, 0.3f, 0.3f, 1f);
        public Color MainBackground => _mainBackground;

        [SerializeField]
        private Color _interactionElementBackground = new Color(0.2f, 0.2f, 0.2f, 1f);
        public Color InteractionElementBackground => _interactionElementBackground;

        [SerializeField]
        private Color _workspaceBackground = new Color(0.35f, 0.35f, 0.35f, 1f);
        public Color WorkspaceBackground => _workspaceBackground;

        [SerializeField]
        private Color _defaultText = Color.white;
        public Color DefaultText => _defaultText;

        [SerializeField]
        private Color _labelText = new Color(0.6f, 0.6f, 0.6f, 1f);
        public Color LabelText => _labelText;

        [SerializeField]
        private Color _placeholderText = new Color(0.6f, 0.6f, 0.6f, 1f);
        public Color PlaceholderText => _placeholderText;
    }
}