using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Snoopy.Web.Models;

namespace Snoopy.Web.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        public NotificationController()
        {
           
        }


        /// <summary>
        /// Action that returns invitations
        /// </summary>
        /// <param name="page">current page no will be bere</param>
        /// <returns></returns>
        public ActionResult Index(int page = 0)
        {
            int noOfrecords = 10;
            var notifications = GetNotifications(page, noOfrecords);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_NotificationList", notifications);
            }
            return View("Index", notifications);
        }


        public int GetNumberOfInvitations()
        {
            return 1;
        }

        /// <summary>
        /// Method returns paged notifications
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<NotificationViewModel> GetNotifications(int page, int noOfRecords)
        {
         List<NotificationViewModel> list=new List<NotificationViewModel>();
            list.Add(new NotificationViewModel(){UserId = "59cdb488-27bf-4e2f-a71c-3cec444898e0"});
            return list ;
        }
    }
}