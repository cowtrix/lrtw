using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace lrtw
{
	public static class TinyAnalytics
	{
		const string ANALYTICS_PATH = "analytics.txt";
		private static HashSet<string> m_dupeCache = new HashSet<string>();	 // clear this on restart, it don't matter

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

		public static uint GetViewCount(string url)
		{
			var d = LoadEntries();
			var e = d.SingleOrDefault(x => x.URL == url);
			if (e == null)
			{
				return 0;
			}
			return e.ViewCount;
		}

		public static void RegisterView(string url, IPAddress ip)
		{
			var d = LoadEntries();
			var e = d.SingleOrDefault(x => x.URL == url);
			
			var hash = Extensions.ComputeSha256Hash($"{url}_{ip}");
			if(m_dupeCache.Contains(hash))
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine($"Duplicate view\t| {DateTime.Now}\t| {ip}\t| {(e != null ? e.ViewCount : 0u)}\t| {url}");
				Console.ResetColor();
				return;
			}
			
			m_dupeCache.Add(hash);
			if (e == null)
			{
				e = new Entry { URL = url };
				d.Add(e);
			}
			e.ViewCount++;
			Console.WriteLine($"View registered\t| {DateTime.Now}\t| {ip}\t| {e.ViewCount}\t| {url}");
			SaveEntries(d);
		}
	}
}
