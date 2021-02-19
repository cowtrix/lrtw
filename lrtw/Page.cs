using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace lrtw
{
	public class Page
	{
		// Data
		public string FilePath { get; }

		// Properties
		public FileInfo FileInfo => new FileInfo(FilePath);
		public DateTime LastEditedUTC => FileInfo.LastWriteTimeUtc;
		public DateTime TimeCreatedUTC { get; }
		public string Title => Path.GetFileNameWithoutExtension(FilePath);
		public string Slug => Title.ToSlug();
		public IEnumerable<string> Lines => File.ReadLines(FilePath).Where(l => !string.IsNullOrEmpty(l));
		public virtual string Content => string.Join("\n\n", Lines);

		public Page(string filePath)
		{
			FilePath = filePath;
			// Look on first line for timestamp override
			var timestamp = Lines.FirstOrDefault()
				?.Split(" ")
				.FirstOrDefault();
			if(timestamp != null && 
				DateTime.TryParseExact(timestamp, "yyyy-MM-dd", 
				CultureInfo.InvariantCulture, DateTimeStyles.None, out var ts))
			{
				TimeCreatedUTC = ts;
			}
			else
			{
				TimeCreatedUTC = FileInfo.CreationTimeUtc;
			}
		}
	}
}
