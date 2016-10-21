using FeedVinc.WEB.UI.Models.ViewModels.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FeedVinc.API.Controllers
{
    [RoutePrefix("api/filters")]
    public class FilterController : BaseApiController
    {
        [Queryable]
        [Route("market")]
        public IQueryable<MarketVM> GetMarketFilter()
        {
            var model = services.projectRepo
                .ToList()
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
                    InvestmentStatus = ""

                })
                .ToList();

            model.ForEach(a => a.CityName = services.cityRepo.
            FirstOrDefault(x => x.ID == a.CityID).CityName);

            model.ForEach(a => a.CountryName = services.countryRepo.
           FirstOrDefault(x => x.ID == a.CountryID).CountryName);

            model.ForEach(a => a.CategoryName = services.projectCategoryRepo.
           FirstOrDefault(x => x.ID == a.CategoryID).CategoryName);

            return model.AsQueryable<MarketVM>();
        }
    }
}
