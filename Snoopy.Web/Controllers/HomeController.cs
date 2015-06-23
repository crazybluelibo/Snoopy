using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FooddbContext;
using Microsoft.AspNet.Identity;
using Snoopy.Web.Models;

namespace Snoopy.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetViewModel());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public HomeViewModel GetViewModel()
        {
            var userid = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var foodContext = new FooddbDataContext();
            var s = foodContext.FoodRecommendations.Where(it => it.UserId == userid);
            var recommand = foodContext.FoodRecommendations.Where(
                 it => it.UserId == userid && it.AddDate.Value.Date == DateTime.Now.Date).ToList().Select(it => foodContext.FoodMains.FirstOrDefault(r => r.ID == it.FoodId)).ToList();
            if (!recommand.Any())
            {
                Random random = new Random();
                var index = random.Next(foodContext.FoodMains.Count() - 5);
                recommand = foodContext.FoodMains.Skip(index).Take(5).ToList();
            }
            var foodviewmodel = new FoodRecommandViewModel();
            foodviewmodel.Recommendations = recommand;
            var homeviewModel=new HomeViewModel();;
            homeviewModel.FoodRecommandViewModel = foodviewmodel;
            return homeviewModel;
        }
    }
}