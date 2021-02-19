using Markdig;
using Markdig.SyntaxHighlighting;
using System;
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

		public static string ToSlug(this string s)
		{
			return Uri.EscapeUriString(s.ToLowerInvariant()
				.Replace(" ", "")
				.Replace("#", "-")
				.Replace("?", ""));
		}
	}
}
