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
		public static List<Blog> AllBlogs;
		public const string GIT_BRANCH = "main";
		public const string GIT_REMOTE = "origin";
		public const string BLOG_PATH = @".\Blog";

		public static void Main(string[] args)
		{
			UpdateAndLoad();

			CreateHostBuilder(args).Build().Run();
		}

		public static void UpdateAndLoad()
		{
			var path = Path.GetFullPath(BLOG_PATH);
			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException($"{path} folder does not exist");
			}

			try
			{
				using (var repo = new Repository(path))
				{
					var remote = repo.Network.Remotes[GIT_REMOTE];
					var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
					Commands.Fetch(repo, remote.Name, refSpecs, null, "");

					var branch = repo.Branches[GIT_BRANCH];
					if (branch == null)
					{
						throw new Exception($"Branch {GIT_BRANCH} did not exist");
					}
					var currentBranch = Commands.Checkout(repo, branch);
				}
			}
			catch(Exception e)
			{
				Console.Write($"Error updating repository: {e.Message}");
			}

			AllBlogs = Directory.GetFiles(path, "*.md")
				.Select(p => new Blog(p))
				.OrderByDescending(b => b.TimeCreatedUTC)
				.ToList();			
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
