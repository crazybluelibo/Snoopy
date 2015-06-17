using Snoopy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FooddbContext;
using Microsoft.AspNet.Identity;
using Snoopy.Data;
namespace Snoopy.Web.Controllers
{
    /// <summary>
    /// 体制
    /// </summary>
    [Authorize]
    public class ConstitutionController : Controller
    {

        public ConstitutionViewModel GetViewModel()
        {
            var foodContext=new  FooddbDataContext();
            ConstitutionViewModel viewmodel = new ConstitutionViewModel();
                 var userid = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var userlist = foodContext.UserSymptoms.Where(it => it.UserId == userid).ToList();
            var promtelist = foodContext.UserSymptomMetas.ToList();
            viewmodel.AvailableItems = promtelist.Select(it => new ConstitutionItemViewModel() { Id = it.SymptomId, Name = it.SymptomName }).ToList();
            viewmodel.Posted = new PostedConstitutions();
            viewmodel.SelectedItems = userlist.Where(it => it.SymptomCheck==true).Select(it => new ConstitutionItemViewModel(){Id = it.SymptomId.Value}).ToList();
            //viewmodel.Posted.Keys = items.Select(it => it.Id).ToArray();
            return viewmodel;
        }

        public ActionResult Index()
        {
            return View(GetViewModel());
        }


        //string[] Keys, PostedConstitutions PostedConstitutions
        public ActionResult Save(ConstitutionViewModel viewmodel)
        {
            var foodContext = new FooddbDataContext();
            var userid = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var userlist = foodContext.UserSymptoms.Where(it => it.UserId == userid).ToList();
            var promtelist = foodContext.UserSymptomMetas.ToList();
            foodContext.UserSymptoms.DeleteAllOnSubmit(userlist);
            foreach (var item in promtelist)
            {
                UserSymptom userSymptom = new UserSymptom();
                userSymptom.ID = foodContext.UserSymptoms.Count() + 1;
                userSymptom.SymptomId = item.SymptomId;
                userSymptom.UserId = userid;
                if (viewmodel.Posted.Keys.Contains(item.SymptomId.ToString()))
                {
                     userSymptom.SymptomCheck = true;
                }
                else
                {
                    userSymptom.SymptomCheck = false;
                }
                foodContext.UserSymptoms.InsertOnSubmit(userSymptom);
            }
            //ViewBag.ReturnUrl = "Home/index";
            //return View();

            return RedirectToAction("Index", "Home"); ;
        }
    }
}