using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class MessageDataAccessBaseModel
    {
        protected UnitOfWork _services;

        public MessageDataAccessBaseModel(UnitOfWork services)
        {
            _services = services;
        }

    }
}