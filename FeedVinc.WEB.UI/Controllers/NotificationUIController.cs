using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.MessageFilter;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.ShareFactory;
using FeedVinc.WEB.UI.ShareFactory.Factories;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class NotificationUIController : BaseUIController
    {
        #region test
        /*
         
            var model = new ShareVM();

            switch (sharetype)
            {
                case 1:
                    model = services.appUserShareRepo.Where(x => x.ID == postid).Select(a => new ShareVM
                    {
                        Location = a.Location,
                        PostMediaPath = a.SharePath,
                        Post = a.Content,
                        MediaTypeID = a.MediaType,
                        ShareTypeID =(byte)a.ShareTypeID,
                        UserID = a.UserID,
                        LikeCount = 0,
                        CommentCount = 0,
                        ShareCount = 0,
                        ShareTypeText = GetShareTypeTextByLanguage((byte)a.ShareTypeID),
                        PrettyDate = DateTimeService.GetPrettyDate(a.ShareDate,LanguageService.getCurrentLanguage),
                        ShareID = a.ID

                    }).FirstOrDefault();

                    model.User = services.appUserRepo.Where(x => x.ID == model.UserID).Select(f => new UserVM
                    {
                        FullName = f.Name + " " + f.SurName,
                        UserCode = f.UserCode,
                        UserSlug = f.UserSlugify,
                        ProfilePhoto = f.ProfilePhoto
                        
                    }).FirstOrDefault();
                    break;
                case 2:
                    model = services.ideaShareRepo.Where(x => x.ID == postid).Select(a => new ShareVM
                    {
                        Post = a.Post,
                        ShareTypeID = a.ShareTypeID,
                        ShareID = a.ID,
                        PostDate = a.PostDate,
                        ShareTypeText = GetShareTypeTextByLanguage((byte)a.ShareTypeID),
                        PrettyDate = DateTimeService.GetPrettyDate(a.PostDate, LanguageService.getCurrentLanguage),
                        LikeCount = 0,
                        CommentCount = 0,
                        ShareCount = 0,
                        ProjectIdeaID = a.ID,
                        ProjectID = a.ProjectID

                    }).FirstOrDefault();

                    model.Idea = services.projectRepo.Where(c => c.ID == model.ProjectID).Select(a => new IdeaShareVM
                    {
                        ProjectName = a.ProjectName,
                        Post = a.SalesPitch,
                        ProjectProfileLogo = a.ProjectProfileLogo,
                        ProjectID = a.ID

                    }).FirstOrDefault();

                    break;
                case 3:

                    
                default:
                    break;
            }

         */

        #endregion

        // GET: ShareUI
        public ActionResult Post(int sharetype, long postid, long notificationid)
        {

            //MessageFilterManager manager = new MessageFilterManager(new NoMessageAccess(services));
            //manager.GetContact("adsad");

            services.shareNotifyUserRepo.Remove(x => x.NotificationID == notificationid && x.UserID == UserManagerService.CurrentUser.ID);
            services.Commit();

            ShareBaseFactory factory = new ShareBaseFactory(services);
            IShare connector = factory.GetObjectInstance(sharetype);
            var model = connector.GetShareObject(postid);
            factory.Dispose();

            return View(model);
        }

        [HttpPost]
        public JsonResult Follow(int id)
        {

            services.followNotifyRepo.Remove(x => x.ID == id);
            services.Commit();


            return Json("OK");
        }
    }
}