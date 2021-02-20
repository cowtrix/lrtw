using System;
using System.Globalization;
using System.IO;

namespace lrtw
{
	public class GalleryContent
	{
		public const string WWWROOT_STRING = @"wwwroot\gallery\";
		public const string TIMESTAMP_MOMENT = "yyyyMMdd_hhmmss";
		public string FilePath { get; }
		public DateTime CreationDate { get; }
		public string URL => Uri.EscapeUriString(FilePath
			.Substring(FilePath.IndexOf(WWWROOT_STRING) + WWWROOT_STRING.Length)
			.Replace("\\", "/"));
		public string Content => StringExtensions.ToHtml($"![embed]({URL})");
		public GalleryContent(string filePath)
		{
			FilePath = filePath;
			var name = Path.GetFileName(FilePath);
			if(name.Length >= TIMESTAMP_MOMENT.Length)
			{
				name = name.Substring(0, TIMESTAMP_MOMENT.Length);
				if (DateTime.TryParseExact(name, TIMESTAMP_MOMENT, null, DateTimeStyles.None, out var ts))
				{
					CreationDate = ts;
				}
			}
			else
			{
				CreationDate = new FileInfo(FilePath).CreationTimeUtc;
			}
		}
	}
}
