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

		public static string RegexReplace(this string input, string pattern, string replacement)
		{
			var rgx = new Regex(pattern);
			return rgx.Replace(input, replacement);
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

		public static string SafeSubstring(this string s, int length)
		{
			if(string.IsNullOrEmpty(s))
			{
				return s;
			}
			if(length > s.Length)
			{
				return s;
			}
			return s.Substring(0, length);
		}
	}
}
