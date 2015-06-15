using Snoopy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
            List<ConstitutionItemViewModel> items = new List<ConstitutionItemViewModel>();
            items.Add(new ConstitutionItemViewModel() { Name = "teste1",Id = "1"});
            items.Add(new ConstitutionItemViewModel() { Name = "teste2" ,Id="2"});
            items.Add(new ConstitutionItemViewModel() { Name = "teste3" ,Id = "3"});
            viewmodel.AvailableItems = items;
            viewmodel.Posted=new PostedConstitutions();
            viewmodel.SelectedItems=new List<ConstitutionItemViewModel>();
            viewmodel.Posted.Keys = items.Select(it => it.Id).ToArray();
            return viewmodel;
        }

        public ActionResult Index()
        {
            return View(GetViewModel());
        }


        //string[] Keys, PostedConstitutions PostedConstitutions
        public ActionResult Save(ConstitutionViewModel viewmodel)
        {

            //ViewBag.ReturnUrl = "Home/index";
            //return View();

            return RedirectToAction("Index", "Home"); ;
        }
    }
}