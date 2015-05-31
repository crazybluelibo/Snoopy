using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snoopy.Data;
using Snoopy.Data.Model;
using Snoopy.Model;

namespace Snoopy.Core
{
    public static class Extends
    {
        public static user_profile Touser_profile(this ApplicationUser user)
        {
            var userprofile = new user_profile();
            userprofile.ID = UserQuery.GetCount("where 1=1") + 1;
            userprofile.user_id = user.Id;
            userprofile.birthday = user.Birthday;
            userprofile.user_name = user.UserName;
            userprofile.user_nickname = user.DisplayName;
            userprofile.phone_number = user.PhoneNumber;
            userprofile.gender = user.Gender;
            userprofile.Email_addr = user.Email;
            return userprofile;

        }

        public static ApplicationUser ToApplicationUser(this user_profile userprofile)
        {
            if (userprofile == null) return null;
            ApplicationUser user=new ApplicationUser();
            //var userprofile = new user_profile();
            //userprofile.ID = UserQuery.GetCount("where 1=1") + 1;
            user.Id = userprofile.user_id;                     ;
            user.Birthday = userprofile.birthday;                        ;
            user.UserName = userprofile.user_name;                      ;
            user.DisplayName = userprofile.user_nickname;              ;
            user.PhoneNumber = userprofile.phone_number;               ;
            user.Gender = userprofile.gender;                       ;
            user.Email = userprofile.Email_addr;                       ;
            return user;
        }
    }
}
