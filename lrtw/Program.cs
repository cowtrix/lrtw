using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace lrtw
{
	public class Program
	{
		public static IEnumerable<Blog> AllBlogs =>
				Directory.GetFiles(Path.GetFullPath(BLOG_PATH), "*.md")
				.Select(p => new Blog(p))
				.OrderByDescending(b => b.TimeCreatedUTC);

		public static IEnumerable<Page> AllPages =>
				Directory.GetFiles(Path.GetFullPath(@".\Pages"), "*.md")
				.Select(p => new Page(p))
				.OrderBy(b => b.Title);

		public static IEnumerable<Gallery> AllGalleries =>
			Directory.GetDirectories(Path.GetFullPath(@".\wwwroot\gallery"))
				.Select(p => new Gallery(p))
				.OrderBy(b => b.GalleryName);

		private static IList<string> AllThoughts
			= File.ReadAllLines(@".\Thoughts.md")
			.Where(l => !string.IsNullOrEmpty(l))
			.Select(l => l.Trim())
			.ToList();

		public const string GIT_BRANCH = "main";
		public const string GIT_REMOTE = "origin";

		public const string BLOG_PATH = @".\wwwroot\blog";

		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});

		public static string GetRandomThought()
		{
			var rnd = new Random();
			var index = rnd.Next(0, AllThoughts.Count());
			return AllThoughts[index];
		}
	}
}
