using Snoopy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Snoopy.Data;
using Snoopy.Data.Model;

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
            ConstitutionViewModel viewmodel = new ConstitutionViewModel();
            var userlist = SymptomQuery.GetList(HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
            var promtelist = SymptomQuery.GetMetaList();
            viewmodel.AvailableItems = promtelist.Select(it => new ConstitutionItemViewModel() { Id = it.symptom_id, Name = it.symptom_name }).ToList();
            viewmodel.Posted = new PostedConstitutions();
            viewmodel.SelectedItems = userlist.Where(it => it.symptom_check).Select(it => new ConstitutionItemViewModel(){Id = it.symptom_id}).ToList();
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
            var userid = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            SymptomQuery.Delete(userid);
            var promtelist = SymptomQuery.GetMetaList();
            foreach (var item in promtelist)
            {
                user_symptom userSymptom = new user_symptom();
                userSymptom.ID = SymptomQuery.GetUserSymptomCount() + 1;
                userSymptom.symptom_id = item.symptom_id;
                userSymptom.user_id = userid;
                if (viewmodel.Posted.Keys.Contains(item.symptom_id.ToString()))
                {
                     userSymptom.symptom_check = true;
                }
                else
                {
                    userSymptom.symptom_check = false;
                }
                SymptomQuery.Insert(userSymptom);
            }
            //ViewBag.ReturnUrl = "Home/index";
            //return View();

            return RedirectToAction("Index", "Home"); ;
        }
    }
}