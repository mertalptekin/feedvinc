using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.UIServices;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class NormalUserChatService : MessageDataAccessBaseModel,IMessage
    {
        public NormalUserChatService(UnitOfWork services) : base(services)
        {
        }

        public NotificationMessageVM SendMessage(NotificationMessagePostVM model)
        {
            var entity = new ApplicationUserMessage()
            {
                Message = model.Message,
                PostDate = DateTime.Now,
                RecieverID = model.RecieverID,
                SenderID = model.SenderID
            };

            _services.appUserMessageRepo.Add(entity);
            _services.Commit();

            var sender = _services.appUserRepo.FirstOrDefault(x => x.ID == model.SenderID);

            var data = new NotificationMessageVM
            {
                SenderID =  model.SenderID,
                RecieverID = model.RecieverID,
                NotificationMessage = sender.Name + " " + sender.SurName + " " +  SiteLanguage.NotificationMessage,
                NotificationPrettyDate = DateTimeService.GetPrettyDate(entity.PostDate, LanguageService.getCurrentLanguage),
                UserMessage = model.Message

            };

            return data;
        }
    }
}