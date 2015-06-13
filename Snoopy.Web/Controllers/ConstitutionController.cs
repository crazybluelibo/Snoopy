using Snoopy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ConstitutionItemViewModel> GetItems()
        {
            List<ConstitutionItemViewModel> items=new List<ConstitutionItemViewModel>();
            items.Add(new ConstitutionItemViewModel(){Key = "teste1"});
            items.Add(new ConstitutionItemViewModel() { Key = "teste2" });
            items.Add(new ConstitutionItemViewModel() { Key = "teste3" });
            return items;
        }

        public ActionResult Index()
        {
            return View(GetItems());
        }
    }
}