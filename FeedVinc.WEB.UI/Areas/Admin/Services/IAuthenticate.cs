using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Services
{
    public interface IAuthenticate
    {
        HttpCookie GetCookie(string cookieName);
        void SetCookie(string cookieName,string cookieValue,bool ispersistant);
        void DeleteCookie(string cookieName);
    }
}
