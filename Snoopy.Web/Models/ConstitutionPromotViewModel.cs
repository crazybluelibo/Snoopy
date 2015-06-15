using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snoopy.Web.Models
{
    /// <summary>
    /// 体质测试
    /// </summary>
    public class ConstitutionItemViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsChecked { get; set; }
    }

    public class ConstitutionViewModel
    {

        public IList<ConstitutionItemViewModel> AvailableItems { get; set; }
        public IList<ConstitutionItemViewModel> SelectedItems { get; set; }

        public PostedConstitutions Posted { get; set; }
    }


    public class PostedConstitutions
    {
        // this array will be used to POST values from the form to the controller
        public string[] Keys { get; set; }
    }
}