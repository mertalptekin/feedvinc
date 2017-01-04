using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.DTO
{
    public class InvestmentLetterDTO
    {
        public long ProjectID { get; set; }
        public long InvestorID { get; set; }
        public long OwnerID { get; set; }

        [Required(ErrorMessage =null, ErrorMessageResourceName ="_MessageValidation",ErrorMessageResourceType =typeof(SiteLanguage))]
        public string Message { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_CustomerSegmentValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string CustomerSegment { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_ProjectOverviewValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string ProjectOverview { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_MarketPotentialValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string MarketPotential { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_ValuePropositionValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string ValueProposition { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_InvestmentStatusValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string InvestmentStatus { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_InvestmentExpectancyValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string InvestmentExpectancy { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_TeamAndCollabrotorsValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string TeamAndCollaborators { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_CompetitorAnalysisValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string CompetitorAnalysis { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "_FinancialConditionValidation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string FinancialCondition { get; set; }

    }
}