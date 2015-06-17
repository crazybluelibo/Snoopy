using FooddbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snoopy.Data
{
   public class ConnectionHelper
   {
       public const string Connection =
          "Data Source=115.28.23.87;Initial Catalog=fooddb;Persist Security Info=True;User ID=mydbuser;Password=Rating123;Pooling=False;charset=utf8;" +
        "MAX Pool Size=2000;Min Pool Size=1;Connection Lifetime=30;";

     

       public ConnectionHelper()
       {
        }
   }
}
