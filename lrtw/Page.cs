using System;
using System.Collections.Generic;
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
		public DateTime TimeCreatedUTC => FileInfo.CreationTimeUtc;
		public string Title => Path.GetFileNameWithoutExtension(FilePath);
		public string Slug => Uri.EscapeUriString(Title.ToLowerInvariant().Replace(" ", ""));
		public IEnumerable<string> Content => File.ReadLines(FilePath).Where(l => !string.IsNullOrEmpty(l));

		public Page(string filePath)
		{
			FilePath = filePath;
		}
	}
}
