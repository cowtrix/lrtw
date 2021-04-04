using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace lrtw
{
	public class Blog : Page
	{
		public Blog(string filePath) : base(filePath)
		{
			Tags = new Regex(@"#(\w+)")
				.Matches(
					File.ReadLines(FilePath).First()
				).Select(m => m.Groups[1].Value)
				.ToArray();
			ThumbnailPath = $"../blog/img/{Title}.png";
			WordCount = string.Join(" ", Content).CountWords();
			if (!File.Exists(Path.Join($".\\wwwroot\\{ThumbnailPath.Substring(2).Replace("/", "\\")}")))
			{
				ThumbnailPath = null;
			}
		}

		public struct Header
		{
			public string Link => Content
				.Trim()
				.RegexReplace(@"[^\w\s]", "")	// Strip punctuation but preserve whitespace
				.Replace(" ", "-")				// Replace whitespace with dashes
				.RegexReplace(@"^\s*\d+.", "")	// Replace numbered list start
				.ToLowerInvariant();
			public string Content;
			public int Indent;
		}

		public string ThumbnailPath { get; }
		public string[] Tags { get; }
		public int WordCount { get; }
		public IEnumerable<Header> Headers =>
			Lines.Where(c => c.StartsWith("#"))
			.Select(c =>
				new Header
				{
					Content = c.Replace("#", ""),
					Indent = c.Count(x => x == '#')
				});
		public override string Content => string.Join("\n\n", Lines.Skip(1));
		public override string Title
		{
			get
			{
				var n = base.Title;
				if (Tags.Any(t => t == "question"))
					n += "?";
				return n;
			}
		}
	}
}