using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.SpeedNetworking;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class SpeedNetworkingUIController : BaseUIController
    {
        // GET: SpeedNetworking
        public ActionResult Index(string projectname,string projectCode)
        {
            var project = services.projectRepo.FirstOrDefault(x => x.ProjectSlugify == projectname && x.ProjectCode == projectCode);

            ViewBag.ProjectID = project.ID;
            ViewBag.ProjectName = project.ProjectSlugify;
            ViewBag.ProjectCode = project.ProjectCode;
            ViewBag.ProjectProfilePath = project.ProjectProfileLogo;

            if (project!=null)
            {
                var data = services.speedNetworkingRepo.FirstOrDefault(x => x.ProjectID == project.ID);

                if (data!=null)
                {
                    SpeedNetworkingVM vm = new SpeedNetworkingVM();
                    vm.ActiveVideoPath = data.ActiveVideoPath;
                    vm.Videos = services.speedNetworkingVideoRepo.Where(x => x.SpeedNetworkinID == data.ID).Select(a => new SpeedNetworkingVideoVM
                    {
                        VideoPath = a.VideoPath,
                        VideoID = a.ID

                    }).ToList();

                    return View(vm);
                }
                else
                {
                    return View(new SpeedNetworkingVM());
                }
               
            }

            return Redirect("/project-profile/" + projectname + "/" + projectCode);
        }

        [HttpPost]
        public JsonResult DeleteVideo(int id)
        {

            services.speedNetworkingVideoRepo.Remove(x => x.ID == id);
            services.Commit();

            return Json("OK");

        }

        [HttpPost]
        public JsonResult SelectActiveVideo(int ProjectID,int VideoID)
        {

            var data = services.speedNetworkingRepo.FirstOrDefault(x=> x.ProjectID== ProjectID);
            data.ActiveVideoPath = services.speedNetworkingVideoRepo.FirstOrDefault(x => x.ID == VideoID).VideoPath;

            services.Commit();

            return Json("OK");
        }


        [HttpPost]
        public JsonResult CreateNewVideo(HttpPostedFileBase video, int projectID)
        {

           string path =  MediaManagerService.Save(new MediaFormatDTO
            { Media = video, MediaType = 1 });


            var speedNetworking = services.speedNetworkingRepo.FirstOrDefault(x => x.ProjectID == projectID);


            if (speedNetworking==null)
            {
                SpeedNetworking entity = new SpeedNetworking();
                entity.ActiveVideoPath = path;
                entity.ProjectID = projectID;

                services.speedNetworkingRepo.Add(entity);
                services.Commit();

                SpeedNetworkingVideo videoEntity = new SpeedNetworkingVideo();
                videoEntity.VideoPath = path;
                videoEntity.SpeedNetworkinID = entity.ID;

                services.speedNetworkingVideoRepo.Add(videoEntity);
                services.Commit();

            }
            else
            {

                speedNetworking.ActiveVideoPath = path;
                services.Commit();

                SpeedNetworkingVideo videoEntity = new SpeedNetworkingVideo();
                videoEntity.VideoPath = path;
                videoEntity.SpeedNetworkinID = speedNetworking.ID;

                services.speedNetworkingVideoRepo.Add(videoEntity);
                services.Commit();

            }


            return Json(path);
        }
    }
}