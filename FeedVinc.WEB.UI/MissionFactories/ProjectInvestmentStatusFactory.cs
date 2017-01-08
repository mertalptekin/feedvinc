using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.DAL.ORM.Entities;

namespace FeedVinc.WEB.UI.MissionFactories
{
    public class ProjectInvestmentStatusFactory : IMissionService
    {
        private UnitOfWork _services;

        public ProjectInvestmentStatusFactory(UnitOfWork _services)
        {
            this._services = _services;
        }

        public int SendMission(int[] selectionIds, int missionid)
        {
            int resultSets = 0;

            if (selectionIds.Contains(0) && selectionIds.Contains(1))
            {
                var projects = _services.projectRepo.ToList().Select(z => new
                {

                    OwnerID = z.UserID,
                    ProjectID = z.ID

                }).ToList();

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
            }
            else if (selectionIds.Contains(0) && !selectionIds.Contains(1))
            {
                var projects = _services.projectRepo.Where(x => x.IsInvested == false).Select(z => new
                {

                    OwnerID = z.UserID,
                    ProjectID = z.ID

                }).ToList();

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
            }
            else if (selectionIds.Contains(1) && !selectionIds.Contains(0))
            {
                var projects = _services.projectRepo.Where(x => x.IsInvested == true).Select(z => new
                {

                    OwnerID = z.UserID,
                    ProjectID = z.ID

                }).ToList();

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
            }


            return resultSets;

        }
    }
}