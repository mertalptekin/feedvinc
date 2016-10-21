using FeedVinc.WEB.UI.Models.ViewModels.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class MarketUIController : BaseUIController
    {

        [HttpGet]
        public async Task<PartialViewResult> Filter(string uri)
        {

            uri = uri.Replace(":", "&");
            IEnumerable<MarketVM> model = null;

            HttpResponseMessage response = await MvcApplication.client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadAsAsync<IEnumerable<MarketVM>>();
            }

            return PartialView("~/Views/MarketUI/MarketPartial/_marketList.cshtml", model);

        }

        // GET: MarketUI
        public ActionResult Index()
        {
            var model = services.projectRepo
                .Where(x => x.FeedPoint > 100)
                .OrderByDescending(x => x.FeedPoint)
                .Take(20)
                .Select(a => new MarketVM
                {
                    ProjectCode = a.ProjectCode,
                    ProjectName = a.ProjectName,
                    ProjectSlug = a.ProjectSlugify,
                    FollowerCount = 0,
                    FeedPoint = a.FeedPoint,
                    ProjectLevel = 0,
                    CityID = (int)a.CityID,
                    CategoryID = a.ProjectCategoryID,
                    CountryID = (int)a.CountyID,
                    SalesPicth = a.SalesPitch,
                    TeamSize = 0,
                    ProjectProfilePhoto = a.ProjectProfileLogo,
                    InvestmentStatus = GetInvestedStatus(a.InvestmentStatus)

                })
                .ToList();

            model.ForEach(a => a.CityName = services.cityRepo.
            FirstOrDefault(x => x.ID == a.CityID).CityName);

            model.ForEach(a => a.CountryName = services.countryRepo.
           FirstOrDefault(x => x.ID == a.CountryID).CountryName);

            model.ForEach(a => a.CategoryName = services.projectCategoryRepo.
           FirstOrDefault(x => x.ID == a.CategoryID).CategoryName);

            ViewBag.ProjectCount = services.projectRepo.Count() / 20;

            return View(model);
        }
    }
}