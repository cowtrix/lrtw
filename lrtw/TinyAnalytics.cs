using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lrtw
{
	public static class TinyAnalytics
	{
		const string ANALYTICS_PATH = "analytics.txt";

		class Entry
		{
			public string URL;
			public uint ViewCount;
		}

		static List<Entry> LoadEntries()
		{
			if (!File.Exists(ANALYTICS_PATH))
			{
				return new List<Entry>();
			}
			return File.ReadAllLines(ANALYTICS_PATH)
				.Select(s => s.Split('\t'))
				.Select(x => new Entry { URL = x[0], ViewCount = uint.Parse(x[1]) })
				.ToList();
		}

		static void SaveEntries(List<Entry> data)
		{
			File.WriteAllLines(ANALYTICS_PATH,
				data.Select(d => $"{d.URL}\t{d.ViewCount}"));
		}

		public static void RegisterView(string url)
		{
			var d = LoadEntries();
			var e = d.SingleOrDefault(x => x.URL == url)
				?? new Entry { URL = url };
			e.ViewCount++;
			SaveEntries(d);
		}
	}
}
