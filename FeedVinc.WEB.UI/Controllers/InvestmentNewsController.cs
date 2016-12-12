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

namespace FeedVinc.WEB.UI.Controllers
{
    public class InvestmentNewsController : BaseUIController
    {
        // GET: InvestmentNews
        public ActionResult Index(int? page)
        {
            var pageIndex = page ?? 1;

            var project = services.projectRepo.Where(x => x.IsInvested == true).ToList();

            var projectIDs = project.Select(a => a.ID);

            var model = services.ideaShareRepo.Where(x => projectIDs.Contains(x.ProjectID)).Select(a => new InvestedNewsVM
            {
                ProjectCode = project.FirstOrDefault(c=> c.ID==a.ProjectID).ProjectCode,
                ProjectName = project.FirstOrDefault(c => c.ID == a.ProjectID).ProjectName,
                ProjectSlug = project.FirstOrDefault(c => c.ID == a.ProjectID).ProjectSlugify,
                PrettyDate = DateTimeService.GetPrettyDate(a.PostDate,LanguageService.getCurrentLanguage),
                ShareCount = a.ShareCount,
                SalesPitch = project.FirstOrDefault(c => c.ID == a.ProjectID).SalesPitch,
                UserID = (long)a.OwnerID,
                ShareID = a.ID

            }).ToList();

            model.ForEach(a => a.LikedCurrentUser = services.ideaShareLikeRepo.Any(x => x.UserID == a.UserID && x.IdeaShareID == a.ShareID));
            model.ForEach(a => a.LikeCount = services.ideaShareLikeRepo.Count(c => c.IdeaShareID == a.ShareID));
            model.ForEach(a => a.CommentCount = services.ideaShareCommentRepo.Count(c => c.IdeaShareID == a.ShareID));


           IPagedList<InvestedNewsVM> vm = model.ToPagedList(pageIndex, 10);


            return View(vm);
        }
    }
}