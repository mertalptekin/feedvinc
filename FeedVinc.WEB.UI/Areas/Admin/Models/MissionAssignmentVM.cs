using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Models
{
    public enum MissionAssignmentType
    {
        Proje_Kategorisine_Göre = 0,
        Proje_Aşamasına_Göre = 1,
        Yatırımı_Aşamasına_Göre = 2,
        Takipçi_Sayısına_Göre = 3,
        FeedPuanına_Göre = 4
    }

    public class MissionAssignmentVM
    {
        public long MissionID { get; set; }
        public MissionAssignmentType MissionAssigmentType { get; set; }
        public string MissionContent { get; set; }
        public int MissionTypeID { get; set; }
        public string MissionTypeName { get; set; }
        public string PrettyDate { get; set; }

    }
}