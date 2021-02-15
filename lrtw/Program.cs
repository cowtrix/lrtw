using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace lrtw
{
	public class Program
	{
		public static List<Blog> AllBlogs;

		public static void Main(string[] args)
		{
			var path = Path.GetFullPath(@".\Blog");
			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException($"{path} folder does not exist");
			}
			AllBlogs = Directory.GetFiles(path, "*.md")
				.Select(p => new Blog(p))
				.ToList();
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
