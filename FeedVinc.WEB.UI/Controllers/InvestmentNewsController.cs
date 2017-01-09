using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Invested;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FeedVinc.WEB.UI.Models.ViewModels.Project;

namespace FeedVinc.WEB.UI.Controllers
{
    public class InvestmentNewsController : BaseUIController
    {
        // GET: InvestmentNews
        public ActionResult Index(int? page)
        {
            var pageIndex = page ?? 1;


            var model = services.InvestmentNewsShareRepo.ToList().Select(a => new InvestedNewsVM
            {

                ShareCount = a.ShareCount,
                ShareID = a.ID,
                ProjectID = a.ProjectID,
                PrettyDate = DateTimeService.GetPrettyDate(a.PrettyDate,LanguageService.getCurrentLanguage),
                InvestmentPrice = a.InvestmentPrice,
                Currency = a.Currency

            }).ToList();

            model.ForEach(a => a.Project = services.projectRepo.Where(c => c.ID == a.ProjectID).Select(v => new ProjectVM
            {
                ProjectName = v.ProjectName,
                ProjectSalesPitch = v.SalesPitch,
                OwnerID = v.UserID,
                ProjectSlugify = v.ProjectSlugify,
                ProjectProfileLogo = v.ProjectProfileLogo,
                ProjectCode = v.ProjectCode

            }).FirstOrDefault());


            model.ForEach(a => a.LikedCurrentUser = services.InvestmentNewsLikeRepo.Any(x => x.ApplicationUserID == a.Project.OwnerID && x.InvestmentNewsID == a.ShareID));
            model.ForEach(a => a.LikeCount = services.InvestmentNewsLikeRepo.Count(c => c.InvestmentNewsID == a.ShareID));
            model.ForEach(a => a.CommentCount = services.InvestmentNewsCommentRepo.Count(c => c.InvestmentNewsID == a.ShareID));


           IPagedList<InvestedNewsVM> vm = model.ToPagedList(pageIndex, 10);


            return View(vm);
        }
    }
}