using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.FactoryPattern;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public enum NotificationType
    {
        Info,
        Warning,
        Error,
        Success
    }

    public class NotificationController : SingletonProvider<NotificationController>
    {
        [SerializeField] private GameObject _notificationPrefab;
        private FactoryProvider<Notification> _factoryProvider = new();
        [SerializeField] private Transform _notificationsTransform;

        [SerializeField] private Color _infoColor;
        [SerializeField] private Color _warningColor;
        [SerializeField] private Color _errorColor;
        [SerializeField] private Color _successColor;


        public void ShowNotification(string message, float duration, NotificationType type)
        {
            var notification = _factoryProvider.Create(_notificationPrefab);
            notification.duration = duration;
            notification.SetMessage(message);
            notification.transform.SetParent(_notificationsTransform);
            switch (type)
            {
                case NotificationType.Info:
                    notification.SetColor(_infoColor);
                    break;
                case NotificationType.Warning:
                    notification.SetColor(_warningColor);
                    break;
                case NotificationType.Error:
                    notification.SetColor(_errorColor);
                    break;
                case NotificationType.Success:
                    notification.SetColor(_successColor);
                    break;
            }
        }
        public void DestroyNotification(Notification notification)
        {
            _factoryProvider.Dismantle(notification);
        }
    }
}