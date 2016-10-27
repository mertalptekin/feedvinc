using FeedVinc.BLL.RepositoryModel;
using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.ShareFactory
{
    public class ShareServiceModel:IDisposable
    {
        protected UnitOfWork _service;

        public ShareServiceModel(UnitOfWork service)
        {
            _service = service;
        }

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}