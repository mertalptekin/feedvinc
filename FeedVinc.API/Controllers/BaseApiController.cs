using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FeedVinc.API.Controllers
{
    public class BaseApiController : ApiController
    {
        protected UnitOfWork services;

        public BaseApiController()
        {
            services = new UnitOfWork();
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose(disposing);
        }
    }
}
