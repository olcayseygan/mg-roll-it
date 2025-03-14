using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Notification : MonoBehaviour
    {
        public float duration = 2f;
        private float _timer = 0f;
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Image _backgroundImage;

        private void Start()
        {
            _timer = duration;
        }

        private void Update()
        {
            _timer = Mathf.Max(0f, _timer - Time.deltaTime);
            if (_timer <= 0)
            {
                NotificationController.I.DestroyNotification(this);
            }
        }

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }

        public void SetColor(Color color)
        {
            _backgroundImage.color = color;
        }

        public void CloseButton_Click()
        {
            NotificationController.I.DestroyNotification(this);
        }
    }
}
