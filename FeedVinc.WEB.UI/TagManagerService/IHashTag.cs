using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.WEB.UI.TagManagerService
{
    interface IHashTag
    {
        bool IsExist { get; }
        long AddTag(string tag,long shareid);
    }
}
