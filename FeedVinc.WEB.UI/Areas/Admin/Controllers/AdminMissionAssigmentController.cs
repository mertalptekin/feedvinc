using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Areas.Admin.Models;
using FeedVinc.WEB.UI.MissionFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridMvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    public class AdminMissionAssigmentController : AdminBaseController
    {

        public ActionResult AddNewMission()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewMission(MissionAssignmentVM model)
        {
            model.MissionTypeID = (int)model.MissionAssigmentType;

            if (ModelState.IsValid)
            {
                var entity = new ProjectMission
                {
                    MissionTypeID = model.MissionTypeID,
                    MissionContent = model.MissionContent,
                    CreateDate = DateTime.Now,

                };

                services.ProjectMissionRepo.Add(entity);
                services.Commit();

                ViewBag.IsSuccess = true;
                ViewBag.Message = "Görev Eklendi";

                return View(model);
            }

            ViewBag.IsSucess = false;
            ViewBag.Message = "Tekrar Deneyin";

            return View();
        }

        public string FindMissionTypeName(MissionAssignmentType type)
        {
            string value = null;

            switch (type)
            {
                case MissionAssignmentType.Proje_Kategorisine_Göre:
                    value = "Proje Kategorisine Göre";
                    break;
                case MissionAssignmentType.Proje_Aşamasına_Göre:
                    value = "Proje Aşamasına Göre";
                    break;
                case MissionAssignmentType.Yatırımı_Aşamasına_Göre:
                    value = "Yatırım Aşamasına Göre";
                    break;
                case MissionAssignmentType.Takipçi_Sayısına_Göre:
                    value = "Takipçi Sayısına Göre";
                    break;
                case MissionAssignmentType.FeedPuanına_Göre:
                    value = "FeedPuan'a Göre";
                    break;
                default:
                    break;
            }

            return value;
        }

        [HttpGet]
        public PartialViewResult Detail(int id)
        {
            var mission = services.ProjectMissionRepo.FirstOrDefault(a => a.ID == id);
            var data = new List<SelectionVM>();

            ViewBag.MissionTypeID = mission.MissionTypeID;

            if (mission.MissionTypeID == 0)
            {
                data = services.projectCategoryRepo.ToList().Select(a => new SelectionVM
                {
                    Name = a.CategoryName,
                    ID = a.ID

                }).ToList();
            }
            else if (mission.MissionTypeID == 1)
            {
                data = services.projectTaskTypeRepo.ToList().Select(a => new SelectionVM
                {
                    Name = a.Name,
                    ID = a.ID

                }).ToList();

            }
            else if (mission.MissionTypeID == 2)
            {
                data = new List<SelectionVM>
                {
                    new SelectionVM
                    {
                        Name = "Yatırım Almayanlar",
                        ID = 0
                    },
                    new SelectionVM
                    {
                        Name = "Yatırım Alanlar",
                        ID = 1
                    }
                    
                };
            }
            else if (mission.MissionTypeID == 3)
            {
                data = new List<SelectionVM>
                {
                    new SelectionVM
                    {
                        Name = "0-250 Takipçi Arası",
                        ID = 0
                    },
                    new SelectionVM
                    {
                        Name = "250-500 Takipçi Arası",
                        ID=1
                    },
                    new SelectionVM
                    {
                        Name = "500-1000 Takipçi Arası",
                        ID = 2
                    },
                    new SelectionVM
                    {
                        Name = "1000 Takipçi ve Üzeri",
                        ID = 3
                    }
                };
            }
            else if (mission.MissionTypeID == 4)
            {
                data = new List<SelectionVM>
                {
                    new SelectionVM
                    {
                        Name = "500 puan ve altı",
                        ID = 0
                    },
                    new SelectionVM
                    {
                        Name = "500 ve 2500 puan arası",
                        ID=1
                    },
                    new SelectionVM
                    {
                        Name = "2500 ve 5000 puan arası",
                        ID = 2
                    },
                    new SelectionVM
                    {
                        Name = "5000 puan ve üzeri",
                        ID = 3
                    }
                };
            }

            return PartialView("~/Areas/Admin/Views/AdminMissionAssigment/partial/_selectionPart.cshtml",data);
        }

        // GET: Admin/AdminMissionAssigment
        public ActionResult Index()
        {

            var model = services.ProjectMissionRepo.ToList().Select(a => new MissionAssignmentVM
            {
                MissionAssigmentType = (MissionAssignmentType)a.MissionTypeID,
                MissionID = a.ID,
                MissionContent = a.MissionContent,
                MissionTypeID = a.MissionTypeID,
                MissionTypeName = FindMissionTypeName((MissionAssignmentType)a.MissionTypeID)

            }).ToList();

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Index(int missionid,int[] userSelectionIds,int typeid)
        {

            MissionFactory factory = new MissionFactory(services);
            var connector = factory.GetInstance((MissionAssignmentType)typeid);

            try
            {
                int resultSets = connector.SendMission(userSelectionIds, missionid);
                ViewBag.DeploymentProjectCount = resultSets;
            }
            catch (Exception)
            {
                ViewBag.Message = "Bu görevin ataması daha önceden yapılmıştır.Yeni Görev oluşturup daha sonra atamasını yapınız.";
            }



            return Index();
        }


    }
}