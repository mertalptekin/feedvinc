using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
       protected  UnitOfWork services;

        public AdminBaseController()
        {
            services = new UnitOfWork();
        }

        public List<SelectListItem> GetProjectTaskType
        {
            get
            {
               return services.projectTaskTypeRepo.ToList().Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.ID.ToString()

                }).ToList();
            }
        }
    }
}