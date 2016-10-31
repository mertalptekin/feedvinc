using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class MessageAccessBaseModel
    {
        protected UnitOfWork _services;

        public MessageAccessBaseModel(UnitOfWork services)
        {
            _services = services;
        }

    }
}