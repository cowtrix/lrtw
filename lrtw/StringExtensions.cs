using Markdig;
using System;
using System.Security.Cryptography;
using System.Text;
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
			while(rgx.IsMatch(input))
			{
				input = rgx.Replace(input, replacement);
			}
			return input;
		}

		public static string StripMarkdown(this string input)
		{
			if(string.IsNullOrEmpty(input))
			{
				return input;
			}
			return Markdown.ToPlainText(input)
				.Replace("^", "");
		}

		public static string ToHtml(string s)
		{
			var pipeline = new MarkdownPipelineBuilder()
				.UseAdvancedExtensions()
				.UsePipeTables()
				.Build();
			return Markdown.ToHtml(s, pipeline);
		}

		public static string ToSlug(this string s)
		{
			return Uri.EscapeUriString(s.ToLowerInvariant()
				.Replace(" ", "")
				.Replace("#", "-")
				.Replace("?", "")
				.Replace("'", ""))
				.Replace("--", "-");
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

		public static string ComputeSha256Hash(string rawData)
		{
			// Create a SHA256   
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
