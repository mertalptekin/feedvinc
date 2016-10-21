using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Event
{
    public class EventPostVM
    {
        [Required(ErrorMessage =null,ErrorMessageResourceName ="Event_Title_validation",ErrorMessageResourceType =typeof(SiteLanguage))]
        public string EventTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string EventDate { get; set; }
        public string EventTime { get; set; }

        [FileValidation(ErrorMessageResourceType =typeof(SiteLanguage), ErrorMessageResourceName ="File_Validation")]

        public HttpPostedFileBase EventProfilePhoto { get; set; }
        public string Location { get; set; }
        public string PhotoPath { get; set; }
    }
}