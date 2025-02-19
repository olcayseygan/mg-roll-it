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

        [SerializeField] private int _selectedIndex = 0;

        private void Awake()
        {
            foreach (var button in _buttons)
            {
                button.onClick.AddListener(() => Button_Click(button));
            }

            UpdateColors();
        }

        public void Button_Click(Button sender)
        {
            if (!_buttons.Contains(sender))
            {
                return;
            }

            var index = _buttons.IndexOf(sender);
            _selectedIndex = index;
            UpdateColors();
            _onIndexChanged.Invoke(index);
        }

        public void SetSelectedIndex(int index)
        {
            if (index < 0 || index >= _buttons.Count)
            {
                return;
            }

            _selectedIndex = index;
            UpdateColors();
        }

        public void UpdateColors()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].image.color = i == _selectedIndex ? _selectedColor : _defaultColor;
            }
        }
    }
}
