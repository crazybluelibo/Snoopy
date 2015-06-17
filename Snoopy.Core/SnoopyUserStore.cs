﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FooddbContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Snoopy.Data;
using Snoopy.Model;

namespace Snoopy.Core
{
    public class SnoopyUserStore : IUserStore<ApplicationUser>, IUserLoginStore<ApplicationUser>, IUserRoleStore<ApplicationUser>, IUserClaimStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserSecurityStampStore<ApplicationUser>
    {
        //private Dictionary<string, ApplicationUser> _users;
        private Dictionary<IdentityUserLogin, ApplicationUser> _logins = new Dictionary<IdentityUserLogin, ApplicationUser>();


        public SnoopyUserStore()
        {
          

        }


        public Task CreateAsync(ApplicationUser user,string password)
        {
            var foodcontext=new FooddbDataContext();
            foodcontext.UserProfiles.InsertOnSubmit(user.Touser_profile());
            return Task.FromResult(0);
        }
       
        public Task CreateAsync(ApplicationUser user)
        {
            var foodcontext = new FooddbDataContext();
            foodcontext.UserProfiles.InsertOnSubmit(user.Touser_profile());
            return Task.FromResult(0);
        }
        public Task UpdateAsync(ApplicationUser user)
        {
            //UserQuery.Update(user.Touser_profile());
            return Task.FromResult(0);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var foodcontext = new FooddbDataContext();

            var t = foodcontext.UserProfiles.FirstOrDefault(it => it.UserId == userId);
            return Task.FromResult<ApplicationUser>(t.ToApplicationUser());
        }

        public void Dispose()
        {
        }

      
        public Task<ApplicationUser> FindAsync(string userName, string phoneName)
        {
            ////var user = new ApplicationUser() { UserName = "adarsh", Id = "402bd590-fdc7-49ad-9728-40efbfe512ec", PasswordHash = "abcd" };
            //foreach (ApplicationUser user in _users.Values)
            //{
            //    if (user.UserName == userName)
            //        return Task.FromResult(user);
            //}
            var foodcontext = new FooddbDataContext();

            var t = foodcontext.UserProfiles.FirstOrDefault(it => it.UserId == userName);
            if (t.PhoneNumber == phoneName)
            {
                return Task.FromResult(t.ToApplicationUser());
            }
            return Task.FromResult<ApplicationUser>(null);
        }
        public Task<ApplicationUser> FindByNameAsync(string userName)
        {

            //}
            var foodcontext = new FooddbDataContext();

            var t = foodcontext.UserProfiles.FirstOrDefault(it => it.UserId == userName);
            //if (t.phone_number == phoneName)
            //{
                return Task.FromResult(t.ToApplicationUser());
           
            return Task.FromResult<ApplicationUser>(null);
        }

        public Task AddLoginAsync(ApplicationUser user, IdentityUserLogin login)
        {
            user.Logins.Add(login);
            _logins[login] = user;
            return Task.FromResult(0);
        }

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
        public Task RemoveLoginAsync(ApplicationUser user, IdentityUserLogin login)
        {
            var logs = user.Logins.Where(l => l.ProviderKey == login.ProviderKey && l.LoginProvider == login.LoginProvider).ToList();
            foreach (var l in logs)
            {
                user.Logins.Remove(l);
                _logins[l] = null;
            }
            return Task.FromResult(0);
        }

        public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindAsync(IdentityUserLogin login)
        {
            if (_logins.ContainsKey(login))
            {
                return Task.FromResult(_logins[login]);
            }
            return Task.FromResult<ApplicationUser>(null);
        }

        
        public Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(ApplicationUser user, IdentityUserRole role)
        {
            user.Roles.Add(role);
            return Task.FromResult(0);
        }

        public Task AddToRoleAsync(ApplicationUser user, string role)
        {
            throw new NotImplementedException();
        }
        public Task RemoveFromRoleAsync(ApplicationUser user, IdentityUserRole role)
        {
            user.Roles.Remove(role);
            return Task.FromResult(0);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return Task.FromResult<IList<string>>(new List<string>());
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return Task.FromResult<bool>(true);
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return Task.FromResult<IList<Claim>>(new List<Claim>());
        }

        public Task AddClaimAsync(ApplicationUser user, IdentityUserClaim claim)
        {
            user.Claims.Add(claim);
            return Task.FromResult(0);
        }

        public Task AddClaimAsync(ApplicationUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(ApplicationUser user, IdentityUserClaim claim)
        {
            user.Claims.Remove(claim);
            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(ApplicationUser user, Claim claim)
        {

            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}
