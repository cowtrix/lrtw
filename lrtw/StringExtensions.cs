using Markdig;
using Markdig.SyntaxHighlighting;
using System.Text.RegularExpressions;

namespace lrtw
{
	public static class StringExtensions
	{
		public static int CountWords(this string s)
		{
			MatchCollection collection = Regex.Matches(s, @"[\S]+");
			return collection.Count;
		}

		public static string ToHtml(string s)
		{
			var pipeline = new MarkdownPipelineBuilder()
				.UseAdvancedExtensions()
				.UseSyntaxHighlighting()
				.Build();
			return Markdown.ToHtml(s, pipeline);
		}
	}
}
