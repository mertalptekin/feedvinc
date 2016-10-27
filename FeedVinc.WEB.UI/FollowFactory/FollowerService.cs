using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.FollowFactory
{
    public class FollowerService:IDisposable
    {
        protected UnitOfWork _service;

        public FollowerService(UnitOfWork service)
        {
            _service = service;
        }

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}