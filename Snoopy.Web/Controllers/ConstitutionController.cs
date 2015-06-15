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
            items.Add(new ConstitutionItemViewModel() { Key = "teste1" });
            items.Add(new ConstitutionItemViewModel() { Key = "teste2" });
            items.Add(new ConstitutionItemViewModel() { Key = "teste3" });
            viewmodel.Items = items;
            return viewmodel;
        }

        public ActionResult Index()
        {
            return View(GetViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
   
        public async Task<ActionResult> Save(ConstitutionViewModel items)
        {
            ViewBag.ReturnUrl = "Home/index";
            //return View();
            Redirect("Home/Index");
            return null;
        }
    }
}