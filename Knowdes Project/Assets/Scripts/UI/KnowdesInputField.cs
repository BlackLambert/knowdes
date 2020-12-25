using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knowdes
{
    public class KnowdesInputField : MonoBehaviour
    {
        private readonly KeyCode[] _submitButtons = new KeyCode[]
        {
            KeyCode.KeypadEnter,
            KeyCode.Return
        };

        [SerializeField]
        private Button _editButton = null;
        [SerializeField]
        private GameObject _editButtonObject = null;
        [SerializeField]
        private Image _editLine = null;
        [SerializeField]
        private TMP_InputField _inputField = null;

        protected virtual void Start()
		{
            _editButton.onClick.AddListener(onEdit);
            setEditMode(false);
        }

		protected virtual void OnDestroy()
		{
            _editButton.onClick.RemoveListener(onEdit);
        }

        protected virtual void Update()
		{
            checkKeyboardSubmit();
        }

        private void checkKeyboardSubmit()
		{
            if (!_inputField.interactable)
                return;
            foreach(KeyCode keyCode in _submitButtons)
			{
                if(Input.GetKeyDown(keyCode))
				{
                    submit(_inputField.text);
                    return;
				}
			}
        }

        private void onEdit()
        {
            setEditMode(true);
            _inputField.Select();
            _inputField.onDeselect.AddListener(submit);
        }

		private void setEditMode(bool editing)
		{
            _editButtonObject.SetActive(!editing);
            _inputField.interactable = editing;
            _editLine.enabled = editing;
        }

        private void submit(string arg0)
        {
            _inputField.onDeselect.RemoveListener(submit);
            setEditMode(false);
        }
    }
}