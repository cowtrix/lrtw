using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lrtw
{
	public class Gallery
	{
		private static string[] SupportedContentFormats = new []
		{
			".jpg",
			".png",
			".mp4",
		};

		public string DirectoryPath { get; }
		public string GalleryName => Path.GetFileName(DirectoryPath);
		public string Slug => GalleryName.ToSlug();
		public string Content => File.Exists(Path.Join(DirectoryPath, $"{GalleryName}.md")) ?
			File.ReadAllText(Path.Join(DirectoryPath, $"{GalleryName}.md")) : "";
		public IEnumerable<GalleryContent> Files =>
			Directory.GetFiles($@".\wwwroot\gallery\{GalleryName}")
			.Where(f => SupportedContentFormats.Contains(Path.GetExtension(f)))
			.Select(p => new GalleryContent(p));
		public Gallery(string name)
		{
			DirectoryPath = name;
		}
	}
}
