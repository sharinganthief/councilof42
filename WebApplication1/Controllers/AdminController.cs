using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication1.Attributes;
using WebApplication1.Models;
using WebGrease.Css.Extensions;

namespace WebApplication1.Controllers
{
	public class WebAdminController : Controller
	{
		public UserManager<ApplicationUser> UserManager { get; private set; }
		public WebAdminController()
			: this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
		{
		}


		public WebAdminController(UserManager<ApplicationUser> userManager)
		{
			UserManager = userManager;
		}
		// GET: Admin
		[AuthorizeDeveloper]
		public ActionResult Index()
		{
			return View();
		}

		[AuthorizeDeveloper]
		public ActionResult ManageUsers()
		{
			ManageUsersModel model = new ManageUsersModel();

			using (ApplicationDbContext dbContext = new ApplicationDbContext())
			{
				model.Users = dbContext.Users.ToList();
			}

			return View(model);
		}

		[AuthorizeDeveloper]
		public ActionResult EditUser(String userId)
		{
			if (String.IsNullOrWhiteSpace(userId))
			{
				return RedirectToAction("ManageUsers");
			}

			EditUserModel model = new EditUserModel();

			using (ApplicationDbContext dbContext = new ApplicationDbContext())
			{
				var user = dbContext.Users.FirstOrDefault(o => o.Id.Equals(userId, StringComparison.InvariantCultureIgnoreCase));

				if (user == null)
				{
					return RedirectToAction("ManageUsers");
				}

				model.User = user;
				model.Roles = dbContext.Roles.Select(o => new RoleDTO() { ID = o.Id, Name = o.Name }).ToList();
				model.UserRoles = user.Roles.Select(o => new RoleDTO() { ID = o.Role.Id, Name = o.Role.Name }).ToList();
				return View(model);
			}

		}

		[AuthorizeDeveloper]
		public ActionResult UpdateUserRoles(PostedRoles postedRoles, string userID)
		{
			if (String.IsNullOrWhiteSpace(userID))
			{
				return RedirectToAction("ManageUsers");
			}

			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);

				UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
				RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

				ApplicationUser user = userManager.FindById(userID);

				if (user == null)
				{
					return RedirectToAction("ManageUsers");
				}

				List<IdentityRole> newRoles = postedRoles.RoleIDs.Select(roleManager.FindById).ToList();

				List<IdentityRole> previousRoles = user.Roles.Select(o => o.Role).ToList();


				foreach (IdentityRole identityRole in newRoles.Where(identityRole => !userManager.IsInRole(user.Id, identityRole.Name)))
				{
					userManager.AddToRole(user.Id, identityRole.Name);
				}

				foreach (IdentityRole previousRole in previousRoles.Where(previousRole => newRoles.All(o => o.Id != previousRole.Id)))
				{
					userManager.RemoveFromRole(user.Id, previousRole.Name);
				}

				context.Users.Attach(user);
				context.Entry(user).State = EntityState.Modified;
				context.Configuration.ValidateOnSaveEnabled = false;
				context.SaveChanges();
				context.Dispose();

			}
			return RedirectToAction("EditUser", new { userId = userID });
		}

		[AuthorizeDeveloper]
		public ActionResult ResetPassword(string userID)
		{
			if (String.IsNullOrWhiteSpace(userID))
			{
				return RedirectToAction("EditUser", new { userId = userID });
			}

			ResetUserPasword(userID);

			return RedirectToAction("EditUser", new { userId = userID });
		}

		private void ResetUserPasword(string userID)
		{
			string password = "password";

			UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());

			UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

			ApplicationUser user = userManager.FindById(userID);

			if (user == null)
			{
				return;
			}

			string hashedNewPassword = userManager.PasswordHasher.HashPassword(password);
			UserStore<ApplicationUser> store = new UserStore<ApplicationUser>();
			user.PasswordHash = hashedNewPassword;
			var update = store.UpdateAsync(user);

			//setPassword.Wait();
			update.Wait();


			var context = userStore.Context as ApplicationDbContext;
			context.Users.Attach(user);
			context.Entry(user).State = EntityState.Modified;
			context.Configuration.ValidateOnSaveEnabled = false;
			context.SaveChanges();
			context.Dispose();
		}
	}
}
