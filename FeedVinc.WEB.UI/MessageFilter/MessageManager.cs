using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class MessageManager
    {
        IMessage _messageService;

        public MessageManager(IMessage messageService)
        {
            _messageService = messageService;
        }

        public NotificationMessageVM SendMessage(NotificationMessagePostVM model)
        {
            return _messageService.SendMessage(model);
        }
    }
}