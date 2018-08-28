using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserCollection.Services.Impl;

namespace UserCollection.Controllers
{
	public class HomeController : Controller
	{
		//private readonly UserService _userService;

		public HomeController()
		{
			//_userService = new UserService();
		}
		public ActionResult Index()
		{
			return RedirectToAction("index", "Help", new { area = "HelpPage" });
		}
	}
}
