using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
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

		public const string GIT_BRANCH = "main";
		public const string GIT_REMOTE = "origin";

		public const string BLOG_PATH = @".\wwwroot\Blog";

		public static void Main(string[] args)
		{
			new Task(async () =>
			{
				while (true)
				{
					try
					{
						UpdateAndLoadBlogs();
					}
					catch (Exception e)
					{
						Console.Write($"Error updating repository: {e.Message}");
					}
					await Task.Delay(TimeSpan.FromMinutes(60));
				}

			}).Start();
			CreateHostBuilder(args).Build().Run();
		}

		public static void UpdateAndLoadBlogs()
		{
			using var repo = new Repository(BLOG_PATH);
			var remote = repo.Network.Remotes[GIT_REMOTE];
			var refSpecs = remote?.FetchRefSpecs.Select(x => x.Specification);
			Commands.Fetch(repo, remote?.Name, refSpecs, null, "");
			var branch = repo.Branches[GIT_BRANCH];
			if (branch == null)
			{
				throw new Exception($"Branch {GIT_BRANCH} did not exist");
			}
			var currentBranch = Commands.Checkout(repo, branch); ;
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
