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

            var project = services.projectRepo.Where(x => x.IsInvested == true).ToList();

            var projectIDs = project.Select(a => a.ID).ToList();

            var model = services.ideaShareRepo.Where(x => projectIDs.Contains(x.ProjectID)).Select(a => new InvestedNewsVM
            {

                ShareCount = a.ShareCount,
                ShareID = a.ID,
                ProjectID = a.ProjectID,
                PrettyDate = DateTimeService.GetPrettyDate(a.PostDate,LanguageService.getCurrentLanguage)

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


            model.ForEach(a => a.LikedCurrentUser = services.ideaShareLikeRepo.Any(x => x.UserID == a.Project.OwnerID && x.IdeaShareID == a.ShareID));
            model.ForEach(a => a.LikeCount = services.ideaShareLikeRepo.Count(c => c.IdeaShareID == a.ShareID));
            model.ForEach(a => a.CommentCount = services.ideaShareCommentRepo.Count(c => c.IdeaShareID == a.ShareID));


           IPagedList<InvestedNewsVM> vm = model.ToPagedList(pageIndex, 10);


            return View(vm);
        }
    }
}