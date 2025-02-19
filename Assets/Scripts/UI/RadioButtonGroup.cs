using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RadioButtonGroup : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons = new();
        [SerializeField] private UnityEvent<int> _onIndexChanged = new();

        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _selectedColor = Color.white;

        private void Awake()
        {
            foreach (var button in _buttons)
            {
                button.onClick.AddListener(() => Button_Click(button));
            }
        }

        public void Button_Click(Button sender)
        {
            if (!_buttons.Contains(sender))
            {
                return;
            }

            var index = _buttons.IndexOf(sender);
            _onIndexChanged.Invoke(index);
        }
    }
}
