using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.InvesterLetter
{
    public class InvestmentLetterDetailVM
    {
        public InvestmentLetterDTO InvestmentLetter { get; set; }
        public InvestmentLetterOwnerVM Owner { get; set; }

        public InvestmentLetterProjectVM Project { get; set; }



    }
}