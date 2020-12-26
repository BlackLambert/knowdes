using System;
using UnityEngine;

namespace Knowdes
{
    public class AppColors : MonoBehaviour
    {
        [SerializeField]
        private AppColorsSettings _colors = null;

        public Color Get(Type type)
		{
            switch(type)
			{
                case Type.MainBackground:
                    return _colors.MainBackground;
                case Type.InteractionElementBackground:
                    return _colors.InteractionElementBackground;
                case Type.WorkspaceBackground:
                    return _colors.WorkspaceBackground;
                case Type.DefaultText:
                    return _colors.DefaultText;
                case Type.LabelText:
                    return _colors.LabelText;
                case Type.PlaceholderText:
                    return _colors.PlaceholderText;
                case Type.Interactable:
                    return _colors.Interactable;
                case Type.Highlighted:
                    return _colors.Highlighted;
                case Type.Selected:
                    return _colors.Selected;
                case Type.Inactive:
                    return _colors.Inactive;
                default:
                    throw new NotImplementedException();
			}
		}

        public enum Type
		{
            MainBackground = 0,
            InteractionElementBackground = 1,
            WorkspaceBackground = 2,

            DefaultText = 100,
            LabelText = 101,
            PlaceholderText = 102,

            Interactable = 200,
            Highlighted = 201,
            Selected = 202,
            Inactive = 203
        }
    }
}