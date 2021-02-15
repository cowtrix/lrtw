using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lrtw.Models;
using System.IO;

namespace lrtw.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		
		public const int PAGE_BLOG_COUNT = 5;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			
		}

		public IActionResult Index([FromQuery] int page = 0, [FromQuery] string tag = null)
		{
			var blogs = Program.AllBlogs
					.Where(b => string.IsNullOrEmpty(tag) || b.Tags.Contains(tag))
					.Skip(page * PAGE_BLOG_COUNT)
					.Take(PAGE_BLOG_COUNT)
					.ToList();
			return View(blogs);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
