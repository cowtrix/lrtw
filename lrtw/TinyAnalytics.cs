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
		const string ROBOTS_PATH = "robots_ip.txt";
		private static HashSet<string> BotList => File.Exists(ROBOTS_PATH) ? File.ReadAllLines(ROBOTS_PATH).ToHashSet() : null;
		private static HashSet<string> m_dupeCache = new HashSet<string>();  // clear this on restart, it don't matter

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
			const int dtLength = -10;
			const int ipLength = -16;
			const int viewLength = -5;

			var d = LoadEntries();
			var e = d.SingleOrDefault(x => x.URL == url);

			var bl = BotList;
			if (bl != null && bl.Contains(ip.ToString()))
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine($"Bot view\t\t| {DateTime.Now,dtLength} | {ip,ipLength} | {(e != null ? e.ViewCount : 0u),viewLength} | {url}");
				Console.ResetColor();
				return;
			}

			var hash = Extensions.ComputeSha256Hash($"{url}_{ip}");
			if (m_dupeCache.Contains(hash))
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine($"Duplicate view\t| {DateTime.Now,dtLength} | {ip,ipLength} | {(e != null ? e.ViewCount : 0u),viewLength} | {url}");
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
			Console.WriteLine($"View registered\t| {DateTime.Now,dtLength} | {ip,ipLength} | {e.ViewCount,viewLength} | {url}");
			SaveEntries(d);
		}
	}
}
