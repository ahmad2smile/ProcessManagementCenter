using System;
using Notifications.Domain;

namespace Notifications.Models
{
    public class NotificationEntity : Notification
    {
        public Guid Id { get; set; }
    }
}