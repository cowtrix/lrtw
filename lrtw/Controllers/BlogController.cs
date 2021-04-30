using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lrtw.Controllers
{
	[Route("blog")]
	public class BlogController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public BlogController(ILogger<HomeController> logger)
		{
			_logger = logger;

		}

		[HttpGet("{id}")]
		public IActionResult ViewBlog(string id)
		{
			var blog = Program.AllBlogs.SingleOrDefault(b => b.Slug == id);
			if(blog != null && HttpContext.Request.Cookies.ContainsKey("anon"))
			{
				TinyAnalytics.RegisterView(id);
			}
			return View(blog);
		}
	}
}
