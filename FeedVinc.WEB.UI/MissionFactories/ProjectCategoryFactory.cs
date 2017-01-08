using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.DAL.ORM.Entities;

namespace FeedVinc.WEB.UI.MissionFactories
{
    public class ProjectCategoryFactory : IMissionService
    {
        private UnitOfWork _services;

        public ProjectCategoryFactory(UnitOfWork _services)
        {
            this._services = _services;
        }

        public int SendMission(int[] selectionIds, int missionid)
        {
            var projects = _services.projectRepo.Where(x => selectionIds.Contains(x.ProjectCategoryID)).Select(z => new {

                OwnerID = z.UserID,
                ProjectID = z.ID

            }).ToList();

            int resultSets = 0;

            for (int i = 0; i < projects.Count(); i++)
            {

                ProjectMissionAssignment entity = new ProjectMissionAssignment();
                entity.OwnerID = projects[i].OwnerID;
                entity.ProjectID = projects[i].ProjectID;
                entity.ProjectMissionID = missionid;
                entity.IsActive = true;

                _services.ProjectMissionAssignmentRepo.Add(entity);
                int result = _services.Commit();
                resultSets = resultSets + result;
            }


            return resultSets;

        }
    }
}