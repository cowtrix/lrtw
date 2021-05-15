using cloudscribe.Syndication.Models.Rss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lrtw
{
	public class RSSFeedBuilder : IChannelProvider
	{
		public string Name => "lrtw";

		public RSSFeedBuilder(IHttpContextAccessor contextAccessor)
		{
			ContextAccessor = contextAccessor;
		}

		protected IHttpContextAccessor ContextAccessor { get; private set; }

		public async Task<RssChannel> GetChannel(CancellationToken cancellationToken = default)
		{
			var allitems = Program.AllBlogs;
			var itemCount = allitems.Count();
			var requestedFeedItems = ContextAccessor.HttpContext?.Request.Query["maxItems"].ToString();
			if (!string.IsNullOrWhiteSpace(requestedFeedItems))
			{
				int.TryParse(requestedFeedItems, out itemCount);
			}
			var channel = new RssChannel
			{
				Title = "lrtw",
				Description = Program.SITE_DESCRIPTION,
				Copyright = "",
				Generator = Name,				
				RemoteFeedUrl = "",
				Link = new System.Uri($"https://{Program.URL}"),
				RemoteFeedProcessorUseAgentFragment = "",
				Items = allitems.Take(itemCount)
					.Select(b => new RssItem
					{
						Link = new System.Uri($"https://{b.URL}"),
						Title = b.Title,
						PublicationDate = b.TimeCreatedUTC,
						Description = Extensions.ToHtml(b.Content),
						Guid = new RssGuid($"https://{b.URL}", true),
					}).ToList()
			};
			return channel;
		}
	}
}
