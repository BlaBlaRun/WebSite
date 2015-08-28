﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BlaBlaRunProject.WebUI.Models;

namespace BlaBlaRunProject.DAL
{
    public class ApplicationDbInitializer
       : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Create App Roles
            CreateRoles(RoleManager);

            CreateUserAdmin(UserManager);
        }

        private static void CreateUserAdmin(UserManager<ApplicationUser> UserManager)
        {
            //Create User=Admin with password=S@tl1nk2014
            string name = "blablarunnow@gmail.com";
            string password = "BlaBlaRunNowWebHill.1";

            var user = new ApplicationUser();
            user.UserName = name;
            user.Email = name;
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, EnumRoles.Admin.ToString());
            }
        }

        private static void CreateRoles(RoleManager<IdentityRole> RoleManager)
        {
            var values = Enum.GetValues(typeof(EnumRoles)).Cast<EnumRoles>();
            IdentityResult roleresult;
            foreach (var rol in values)
            {
                if (!RoleManager.RoleExists(rol.ToString()))
                    roleresult = RoleManager.Create(new IdentityRole(rol.ToString()));
            }
        }
    }
}