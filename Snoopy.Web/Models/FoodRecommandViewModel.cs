using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FooddbContext;

namespace Snoopy.Web.Models
{
    public class FoodRecommandViewModel 
    {
        public IList<FoodMain> Recommendations { get; set; }

        public string[] SelectedItems { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
} 