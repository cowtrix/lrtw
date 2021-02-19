using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lrtw.Controllers
{
	public class GalleryViewModel
	{
		public bool Filter(GalleryContent f)
		{
			if(!string.IsNullOrEmpty(Year) && f.CreationDate.Year.ToString() != Year)
			{
				return false;
			}
			return true;
		}

		public Gallery Gallery;
		public string Year;
		public string Month;
		public int Page;
	}

	[Route("gallery")]
	public class GalleryController : Controller
	{
		private readonly ILogger<GalleryController> _logger;

		public GalleryController(ILogger<GalleryController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View(Program.AllGalleries);
		}

		[HttpGet("{id}")]
		public IActionResult ViewGallery(string id, 
			[FromQuery]string year = "",
			[FromQuery] string month = "",
			[FromQuery] int page = 1)
		{
			var gallery = Program.AllGalleries
				.SingleOrDefault(b => string.Equals(b.Slug, id, StringComparison.OrdinalIgnoreCase));
			return View(new GalleryViewModel
			{
				Gallery = gallery,
				Year = year,
				Month = month,
				Page = page,
			});
		}
	}
}
