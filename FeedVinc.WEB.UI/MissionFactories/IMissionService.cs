using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.WEB.UI.MissionFactories
{
    public interface IMissionService
    {
        int SendMission(int[] selectionIds,int missionid);
    }
}
