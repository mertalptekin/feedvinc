using FeedVinc.BLL.Services;
using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Filter;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Models.ViewModels.Message;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.ShareCommentFactory;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using app = FeedVinc.WEB.UI.MvcApplication;

namespace FeedVinc.WEB.UI.Controllers
{
    [LoginControl]
    public class BaseUIController : Controller
    {
        protected UnitOfWork services;
        protected UserVM _currentUser;


        public BaseUIController()
        {
            services = new UnitOfWork();
            _currentUser = UserManagerService.CurrentUser;

        }


        [ChildActionOnly]
        public PartialViewResult GetUserMessage()
        {
            //bana mesaj atanların idleri
            //kendimi alıcı olarak kabul ettim
            var model = new MessageWrapperVM();
            model.Contacts = new List<MessageFilter.MessageContactVM>();
            model.MessageDetails = new List<MessageDetailVM>();

            var senderIDs = services.appUserMessageRepo.Where(x => x.RecieverID == UserManagerService.CurrentUser.ID).GroupBy(v => v.SenderID).Select(a => a.Key).ToList();

            var LastmessageIDs = new List<long>();

            foreach (var item in senderIDs)
            {
                var id = services.appUserMessageRepo.Where(x => x.SenderID == item).OrderByDescending(x => x.MessageID).Take(1).FirstOrDefault().MessageID;
                LastmessageIDs.Add(id);
            }

            model.LastMessages = services.appMessageRepo
                .Where(a => LastmessageIDs.Contains(a.ID))
                .OrderByDescending(c => c.ID)
                .Select(a => new MessageVM
                {
                    LastMessage = a.Message,
                    LastLook = DateTimeService.GetPrettyDate(a.PostDate, LanguageService.getCurrentLanguage),
                    MessageID = a.ID
                })
            .ToList();

            model.LastMessages.ForEach(a => a.SenderID = services.appUserMessageRepo.FirstOrDefault(c => c.MessageID == a.MessageID).SenderID);

            model.LastMessages
                .ForEach(a => a.User = services.appUserRepo.Where(x => x.ID == a.SenderID)
                .Select(f => new MessageUserVM
                {
                    UserName = f.Name + " " + f.SurName,
                    UserProfilePhoto = f.ProfilePhoto
                })
                .FirstOrDefault());

            //senderların gönderdiği mesajların detayını çekmek lazım;

            //hesabı aktif olan kullanıcıya gönderilen mesajların idsi
            //ve aktif olan kullanıcının gönderdiği mesajlarınidsi
            var currentUserID = UserManagerService.CurrentUser.ID;

            var senderMessageIds = services.appUserMessageRepo.Where(a => senderIDs.Contains(a.SenderID) && a.RecieverID == currentUserID).Select(a => a.MessageID );

            var reciverMessageIds = services.appUserMessageRepo.Where(a => a.SenderID == currentUserID && senderIDs.Contains(a.RecieverID)).Select(c=>  c.MessageID );

            var senderMessages = services.appMessageRepo.Where(a => senderMessageIds.Contains(a.ID)).Select(v => new MessageDetailVM
            {
                Message = v.Message,
                IsSent = true,
                IsRecieved = false,
                MessageID = v.ID,
                PrettyPostDate = DateTimeService.GetPrettyDate(v.PostDate, LanguageService.getCurrentLanguage)

            }).ToList();

            senderMessages.ForEach(a => a.SenderID = services.appUserMessageRepo.Where(x => x.MessageID == a.MessageID).FirstOrDefault().SenderID);

            var recieverMessages = services.appMessageRepo.Where(a => reciverMessageIds.Contains(a.ID)).Select(v => new MessageDetailVM
            {
                Message = v.Message,
                IsSent = false,
                IsRecieved = true,
                MessageID = v.ID,
                PrettyPostDate = DateTimeService.GetPrettyDate(v.PostDate, LanguageService.getCurrentLanguage)

            }).ToList();

            recieverMessages.ForEach(a => a.SenderID = services.appUserMessageRepo.Where(x => x.MessageID == a.MessageID).FirstOrDefault().SenderID);

            model.MessageDetails.AddRange(recieverMessages);
            model.MessageDetails.AddRange(senderMessages);
            model.MessageDetails = model.MessageDetails.OrderByDescending(x => x.MessageID).ToList();

            return PartialView("~/Views/Shared/Partial/_userMessageDropDown.cshtml",model);
        }

        [ChildActionOnly]
        public PartialViewResult GetFollowNotificationTop5()
        {

            var model = services.followNotifyRepo
                .Where(x => x.OwnerID == UserManagerService.CurrentUser.ID)
                .OrderByDescending(x => x.PostDate)
                .Take(10)
                .Select(a => new NotificationFollowVM
                {
                    NotificationName = a.OwnerName,
                    NotificationText = a.NotificationText,
                    Link = a.Link,
                    ProfilePhoto = a.NotificationPhotoPath

                })
            .ToList();



            return PartialView("~/Views/Shared/Partial/_notificationFollowDropDown.cshtml", model);
        }

        [ChildActionOnly]
        public PartialViewResult GetFollowNotification()
        {

            var model = services.followNotifyRepo
                .Where(x => x.OwnerID == UserManagerService.CurrentUser.ID)
                .Select(a => new NotificationFollowVM
                {
                    NotificationName = a.OwnerName,
                    NotificationText = a.NotificationText,
                    Link = a.Link,
                    ProfilePhoto = a.NotificationPhotoPath
                })
            .ToList();

            return PartialView("~/Views/Shared/Partial/Modal/_followerNotificationModal.cshtml", model);
        }

        [ChildActionOnly]
        public PartialViewResult GetShareNotificationCurrentUserProfile()
        {
            var notificationIDs = services.shareNotifyUserRepo
                .Where(x => x.UserID == UserManagerService.CurrentUser.ID)
                .Select(c => c.NotificationID)
                .ToList();

            var model = services.shareNotifyRepo
                .Where(x => notificationIDs.Contains(x.ID))
                .Select(a => new NotificationShareVM
                {
                    NotificationText = a.NotificationText,
                    ProfilePhotoPath = a.NotificationPhotoPath,
                    SharePrettyDate = DateTimeService.GetPrettyDate(a.PostDate, LanguageService.getCurrentLanguage),
                    ShareNotificationID = a.ID,
                    ShareProfileLink = a.Link,
                    ShareProfileName = a.OwnerName

                })
            .ToList();

            return PartialView("~/Views/Shared/Partial/Modal/_shareNotificationModal.cshtml", model);
        }

        [ChildActionOnly]
        public PartialViewResult GetShareNotificationCurrentUserProfileTop5()
        {
            var notificationIDs = services.shareNotifyUserRepo
              .Where(x => x.UserID == UserManagerService.CurrentUser.ID)
              .Select(c => c.NotificationID)
              .ToList();

            var model = services.shareNotifyRepo
                .Where(x => notificationIDs.Contains(x.ID))
                .OrderByDescending(x => x.PostDate)
                .Take(5)
                .Select(a => new NotificationShareVM
                {
                    NotificationText = a.NotificationText,
                    ProfilePhotoPath = a.NotificationPhotoPath,
                    SharePrettyDate = DateTimeService.GetPrettyDate(a.PostDate, LanguageService.getCurrentLanguage),
                    ShareNotificationID = a.ID,
                    ShareProfileLink = a.Link,
                    ShareProfileName = a.OwnerName

                })
            .ToList();

            return PartialView("~/Views/Shared/Partial/_notificationDropDown.cshtml", model);
        }

        [HttpPost]
        public ActionResult Share(SharePostVM model)
        {

            if (ModelState.IsValid)
            {
                if (model.MediaPhoto != null || model.MediaVideo != null)
                {
                    MediaFormatDTO mediaDTO = new MediaFormatDTO
                    {
                        MediaType = model.MediaPhoto != null ? 0 : 1,
                        Media = model.MediaPhoto != null ? model.MediaPhoto : model.MediaVideo,
                    };

                    model.MediaPath = MediaManagerService.Save(mediaDTO);
                    model.MediaTypeID = mediaDTO.MediaType;

                }

                model.ShareTitle = SiteLanguage._AROUNDME;
                ApplicationUserShare Usershare = new ApplicationUserShare
                {
                    Location = model.Location,
                    Content = model.Post,
                    ShareTypeID = model.ShareTypeID,
                    IsActive = true,
                    SharePath = model.MediaPath,
                    MediaType = model.MediaTypeID,
                    ShareDate = DateTime.Now,
                    UserID = UserManagerService.CurrentUser.ID
                };
                services.appUserShareRepo.Add(Usershare);
                int ID = services.Commit();


                SharePostDTO dto = new SharePostDTO
                {
                    FeedID = Usershare.ID,
                    Post = model.Post,
                    Location = model.Location,
                    MediaPath = model.MediaPath,
                    MediaTypeID = model.MediaTypeID,
                    ShareTypeID = model.ShareTypeID,
                    ShareTitle = SiteLanguage.Around_Me,
                    User = _currentUser,
                    ProjectShare = services.projectRepo.Where(x => x.ID == ID).Select(a => new ProjectSharePostDTO
                    {

                        ProjectName = a.ProjectName,
                        ProjectProfilePath = a.ProjectProfileLogo,
                        ProjectSlugify = a.ProjectSlugify,
                        ProjectID = a.ID

                    }).FirstOrDefault(),
                    PrettyDate = DateTimeService.GetPrettyDate(DateTime.Now, LanguageService.getCurrentLanguage),
                    Validation = new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.Shared_your_Post }
                };

                return PartialView("~/Views/HomeUI/FeedPartial/_feed_around_me.cshtml", dto);

            }


            return Json(new ValidationDTO { IsValid = false, ErrorMessage = SiteLanguage.Post_Validation });

        }


        [HttpGet]
        public List<ShareCommentVM> GetCommentsByShareID(long shareID, string shareType)
        {
            ShareCommentFactoryModel factory = new ShareCommentFactoryModel(services);
            var connector = factory.CreateObjectInstance(shareType);

            var data = connector.GetCommmentsByShareID(shareID);

            return data;
        }

        public IEnumerable<SelectListItem> GetProjectCategoryDropDown(byte? selectedCategoryID = 0)
        {
            var model = services.projectCategoryRepo.Where(x => x.Lang == LanguageService.getCurrentLanguage).Select(a => new SelectListItem { Text = a.CategoryName, Value = a.ID.ToString(), Selected = a.ID == selectedCategoryID ? true : false }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.Project_Category_Validation, Value = "0", Selected = selectedCategoryID != null ? true : false });

            return model.OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetCountryDropDown(int? selectedCountryID = 0)
        {
            var model = services.countryRepo.ToList().Select(a => new SelectListItem { Text = a.CountryName, Value = a.ID.ToString(), Selected = (a.ID == selectedCountryID ? true : false) }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.County_Validation, Value = "0", Selected = selectedCountryID == null ? true : false });

            return model.OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetMyProjectsDropDown()
        {
            var projectIDs = services.projectRepo.
                Where(x => x.UserID == UserManagerService.CurrentUser.ID).
                Select(x => x.ID).
                ToList();

            var model = services.projectRepo.
                Where(x => projectIDs.Contains(x.ID)).
                Select(a => new SelectListItem
                {
                    Text = a.ProjectName,
                    Value = a.ID.ToString()
                }).
                OrderBy(x => x.Text).
                ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.Project_Validation, Value = "0", Selected = true });

            return model;
        }

        public IEnumerable<SelectListItem> GetUserTypeDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = SiteLanguage.Developer,
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = SiteLanguage.Financier,
                    Value = "2",
                    Selected = false
                },
                new SelectListItem
                {
                    Text = SiteLanguage.Entrepreneur,
                    Value="3",
                    Selected=false
                }
            };
        }

        [ChildActionOnly]
        public PartialViewResult GetFilterPartial()
        {
            var model = new FilterVM
            {
                ProjectCategory = GetProjectCategoryDropDown(),
                Country = GetCountryDropDown(),
                City = GetCityDropDown(1)
            };

            return PartialView("~/Views/Shared/Partial/_filters.cshtml", model);
        }

        public string GetInvestedStatus(int investedStatusID)
        {
            string output = "";

            switch (investedStatusID)
            {
                case 1:
                    output = SiteLanguage.ProjectInvested_2nd_Not_Invested;
                    break;
                case 2:
                    output = SiteLanguage.ProjectInvested_1st_lap;
                    break;
                case 3:
                    output = SiteLanguage.ProjectInvested_2nd_lap;
                    break;
            }

            return output;
        }

        public string GetProjectStatus(int projectStatusID)
        {
            string output = "";

            switch (projectStatusID)
            {
                case 1:
                    output = SiteLanguage.ProjectStatus_Newer;
                    break;
                case 2:
                    output = SiteLanguage.ProjectStatus_Development_of_Phase;
                    break;
                case 3:
                    output = SiteLanguage.ProjectStatus_Ready_for_Publication;
                    break;
            }

            return output;
        }

        public IEnumerable<SelectListItem> GetCityDropDown(int? countryID, int? selectedCityID = 0)
        {
            var model = services.cityRepo.Where(x => x.CountryID == countryID).Select(a => new SelectListItem { Text = a.CityName, Value = a.ID.ToString(), Selected = (a.ID == (int)selectedCityID ? true : false) }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.City_Validation, Value = "0", Selected = (selectedCityID == null ? true : false) });

            return model.OrderBy(x => x.Value);
        }

        [ChildActionOnly]
        public PartialViewResult GetNavbar()
        {
            IEnumerable<byte> appMenuIDs = services.appMenuDetailRepo.Where(x => x.UserTypeID == UserManagerService.CurrentUser.UserTypeID).Select(x => x.ApplicationMenuID).ToList();

            var model = services.appMenuRepo.Where(x => x.Lang == LanguageService.getCurrentLanguage && appMenuIDs.Contains(x.ID)).Select(a => new ApplicationUserMenuVM
            {
                MenuName = a.Name,
                RedirectURL = a.Url


            });

            return PartialView("~/Views/HomeUI/FeedPartial/_navbar.cshtml", model);
        }


        public string GetUserTypeString(byte userTypeID)
        {
            switch (userTypeID)
            {
                case 1:
                    return SiteLanguage.Developer;
                case 2:
                    return SiteLanguage.Financier;
                case 3:
                    return SiteLanguage.Entrepreneur;
                default:
                    return null;
            }
        }

        [ChildActionOnly]
        public PartialViewResult GetInvestedProjects()
        {
            var model = services.projectRepo.Where(x => x.IsInvested == true).Select(z => new InvestedProjectVM
            {

                ProjectCode = z.ProjectCode,
                ProjectSlug = z.ProjectSlugify,
                ProjectName = z.ProjectName,
                ProjectProfilePhoto = z.ProjectProfileLogo,
                SalesPitch = z.SalesPitch,
                CreateDate = z.CreateDate

            }).OrderByDescending(x => x.CreateDate).Take(10).ToList();

            return PartialView("~/Views/Shared/Partial/_InvestedProject.cshtml", model);
        }

        [ChildActionOnly]
        public PartialViewResult GetLastestLaunch()
        {
            var model = services.projectLaunchRepo.
                ToList().
                Select(f => new LastestLaunchVM
                {
                    Information = f.Information,
                    LaunchProfilePhoto = f.MediaPath,
                    PostDate = f.PostDate,
                    ProjectID = f.ProjectID

                }).OrderBy(x => x.PostDate).Take(10).ToList();

            model.ForEach(a => a.ProjectName = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectName);

            model.ForEach(a => a.ProjectSlug = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectSlugify);

            model.ForEach(a => a.ProjectCode = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectCode);

            return PartialView("~/Views/Shared/Partial/_lastestLaunch.cshtml", model);
        }

        public string GetShareTypeTextByLanguage(byte shareTypeID)
        {
            switch (shareTypeID)
            {
                case 1:
                    return SiteLanguage._AROUNDME;
                case 2:
                    return SiteLanguage._IDEA;
                case 3:
                    return SiteLanguage._STORY_TELLING;
                case 4:
                    return SiteLanguage._FEEDBACK;
                case 5:
                    return SiteLanguage._LAUNCH;
                case 6:
                    return SiteLanguage._COMMUNITY;
                default:
                    return null;
            }
        }


        [OverrideActionFilters]
        public ActionResult ChangeLanguage(string language = "en-US")
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            HttpCookie cookie = new HttpCookie("LanguageInfo");
            cookie.Value = language;
            Response.Cookies.Add(cookie);

            return Redirect("/home");
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose(disposing);
        }
    }
}