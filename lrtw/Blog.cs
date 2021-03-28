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

		public string ThumbnailPath { get; }
		public string[] Tags { get; }
		public int WordCount { get; }
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