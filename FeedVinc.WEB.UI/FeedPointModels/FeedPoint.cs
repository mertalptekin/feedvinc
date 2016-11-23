using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.FeedPointModels
{
    public abstract class FeedPoint
    {
        protected long _currentFeedPoint;
        protected long _currentProjectID;
        public long CurrentPoint { get { return this._currentFeedPoint; } }
       
        protected UnitOfWork _services;

        public FeedPoint(long projectID)
        {
            _services = new UnitOfWork();
            _currentProjectID = projectID;
            
        }

        public abstract void GetFeedPoint();
    }
}