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
		public const string PAGE_KEY = "page";
		public const string RESULT_SIZE = "resultsize";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index([FromQuery] int page = 1, [FromQuery] string tag = null)
		{
			page = Math.Max(1, page);
			var all = Program.AllBlogs
					.Where(b => string.IsNullOrEmpty(tag) || b.Tags.Contains(tag));
			var paginated = all.Skip((page - 1) * PAGE_BLOG_COUNT)
					.Take(PAGE_BLOG_COUNT)
					.ToList();
			ViewData[PAGE_KEY] = page;
			ViewData[RESULT_SIZE] = all.Count();
			return View(paginated);
		}

		[Route("compact")]
		public IActionResult Compact([FromQuery] string tag = null)
		{
			return View(Program.AllBlogs
					.Where(b => string.IsNullOrEmpty(tag) || b.Tags.Contains(tag)));
		}

		[HttpGet("{id}")]
		public IActionResult Page([FromRoute] string id)
		{
			return View(Program.AllPages.SingleOrDefault(p => string.Equals(id, p.Title, StringComparison.OrdinalIgnoreCase)));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
