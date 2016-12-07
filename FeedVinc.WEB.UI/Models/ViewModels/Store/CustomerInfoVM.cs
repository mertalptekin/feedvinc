using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Store
{
    public class CustomerInfoVM
    {
        public string PaymentType { get; set; }
        public string CreditCardOwner { get; set; }
        public string CardNumber { get; set; }
        public string SecurityCode { get; set; }
        public string ValidThru { get; set; }
        public bool Is3DSecurity { get; set; }
        public string BillingAddress { get; set; }

    }
}