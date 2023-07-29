using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Rent.Models
{
    public class HistoryManager : ApplicationUser
    {
        public int RestrictedUsers { get; set; }
        public int RentalReplacementRequests { get; set; }

        public static void Seed(ApplicationDbContext dbContext)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));
            var roleUserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));

            if (!roleManager.RoleExists("HistoryManager"))
            {
                var modRole = new IdentityRole();
                modRole.Name = "HistoryManager";
                roleManager.Create(modRole);

                var historyManager = new HistoryManager
                {
                    UserName = "HistoryManager",
                    Email = "manage@history.com"
                };
                string password = "abc123pass";
                var HistManager = roleUserManager.Create(historyManager, password);
                if (HistManager.Succeeded)
                {
                    roleUserManager.AddToRole(historyManager.Id, "HistoryManager");
                }
            }
        }
    }

    public class HistoryManagerAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Rent/RentalHistories/AccessDenied");
            }
        }
    }
}