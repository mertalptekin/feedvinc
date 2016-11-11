using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.TagManagerService
{
    public class TagManager : IHashTag
    {
        private string _hashTag;

        public TagManager(string tag)
        {
            _hashTag = tag;
        }

        public bool IsExist
        {

            get
            {
                using (ProjectContext context = new ProjectContext())
                {
                    return context.AppUserShareTags.Any(x => x.HashTag == this._hashTag);
                }
            }

        }

        public long AddTag(string tag, long shareid)
        {
            if (!IsExist)
            {
                using (ProjectContext context = new ProjectContext())
                {
                    var entity = new ApplicationUserShareTag();
                    entity.HashTag = tag;
                    entity.IsActive = true;

                    context.AppUserShareTags.Add(entity);
                    context.SaveChanges();

                    var _tagDetail = new ApplicationUserShareTagDetail();
                    _tagDetail.ApplicationUserShareID = shareid;
                    _tagDetail.ApplicationUserShareTagID = entity.ID;
                    _tagDetail.IsActive = true;

                    context.AppUserShareTagDetails.Add(_tagDetail);
                    context.SaveChanges();

                    return entity.ID;
                }
            }

            return 0;
        }

        

    }
}