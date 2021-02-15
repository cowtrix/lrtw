using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
	}

	public class Blog
	{
		public Blog(string filePath)
		{
			FilePath = filePath;
			Tags = new Regex(@"#(\w+)")
				.Matches(
					File.ReadLines(FilePath).First()
				).Select(m => m.Groups[1].Value)
				.ToArray();
			ImagePath = $"../Blog/img/{Slug}.png";
			Content = File.ReadLines(FilePath).Where(l => !string.IsNullOrEmpty(l)).ToList();
			WordCount = string.Join(" ", Content).CountWords();
			if (!File.Exists(Path.Join($".\\wwwroot\\{ImagePath.Substring(2).Replace("/", "\\")}")))
			{
				ImagePath = null;
			}
		}



		public string FilePath { get; }
		public string Title => Path.GetFileNameWithoutExtension(FilePath);
		public string Slug => Uri.EscapeUriString(Title.ToLowerInvariant().Replace(" ", ""));
		public string ImagePath { get; }
		public FileInfo FileInfo => new FileInfo(FilePath);
		public DateTime LastEditedUTC => FileInfo.LastWriteTimeUtc;
		public DateTime TimeCreatedUTC => FileInfo.CreationTimeUtc;
		public IEnumerable<string> Content { get; }
		public string[] Tags { get; }
		public int WordCount { get; }
	}
}
