using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.DAL.ORM.Entities;

namespace FeedVinc.WEB.UI.MissionFactories
{
    public class ProjectFollowFactory : IMissionService
    {
        private UnitOfWork _services;

        public ProjectFollowFactory(UnitOfWork _services)
        {
            this._services = _services;
        }

        public int SendMission(int[] selectionIds, int missionid)
        {
            int selection = selectionIds[0];


            int resultSets = 0;

            var query = (from p in _services.projectRepo.ToList()
                         join f in _services.projectFollowRepo.ToList() on p.ID equals f.ProjectID
                         select new
                         {
                             p.ID,
                             f.UserID,

                         }).GroupBy(y => y.ID).Select(z => new
                         {
                             ProjectID = z.Key,
                             OwnerID = z.FirstOrDefault().UserID,
                             Count = z.Count()

                         }).ToList();



            if (selection == 0)
            {
                var projects = query.Where(y => y.Count <= 250).ToList();

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
            else if (selection == 1)
            {
                var projects = query.Where(y => y.Count > 250 && y.Count <= 500).ToList();

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
            else if (selection == 2)
            {
                var projects = query.Where(y => y.Count > 500 && y.Count <= 1000).ToList();

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
            else
            {
                var projects = query.Where(y => y.Count > 1000).ToList();

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