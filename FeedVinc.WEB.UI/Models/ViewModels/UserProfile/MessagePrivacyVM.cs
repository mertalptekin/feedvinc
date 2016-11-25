using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.UserProfile
{
    public class MessagePrivacyVM
    {
        public bool IsNoMessageAccess { get; set; }
        public bool IsPrivateMessageAccess { get; set; }
        public bool IsPublicMessageAccess { get; set; }

    }
}