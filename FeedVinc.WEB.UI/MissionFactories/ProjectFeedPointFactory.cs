using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.DAL.ORM.Entities;

namespace FeedVinc.WEB.UI.MissionFactories
{

    public class ProjectFeedPointFactory : IMissionService
    {
        private UnitOfWork _services;

        public ProjectFeedPointFactory(UnitOfWork _services)
        {
            this._services = _services;
        }

        int IMissionService.SendMission(int[] selectionIds, int missionid)
        {

            int selection = selectionIds[0];

            int resultSets = 0;

            if (selection==0)
            {
                var projects = _services.projectRepo.Where(x => x.FeedPoint <= 500).ToList().Select(a=>new
                {
                    ProjectID = a.ID,
                    OwnerID = a.UserID

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


                return resultSets;

            }
            else if (selection==1)
            {
                var projects = _services.projectRepo.Where(x => x.FeedPoint > 500 && x.FeedPoint<=2500).ToList().Select(a => new
                {
                    ProjectID = a.ID,
                    OwnerID = a.UserID

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


                return resultSets;

            }
            else if (selection==2)
            {
                var projects = _services.projectRepo.Where(x => x.FeedPoint > 2500 && x.FeedPoint <= 5000).ToList().Select(a => new
                {
                    ProjectID = a.ID,
                    OwnerID = a.UserID

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


                return resultSets;

            }
            else 
            {
                var projects = _services.projectRepo.Where(x => x.FeedPoint > 5000).ToList().Select(a => new
                {
                    ProjectID = a.ID,
                    OwnerID = a.UserID

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


                return resultSets;
            }

        }
    }
}