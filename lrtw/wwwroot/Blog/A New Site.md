2021-02-19 #blog

I've been unhappy with my portfolio site for a while now, so I made a new one. A fair few years ago I bought hosting with a hosting company InMotion and just set up a  Wordpress instance, and that has served its purpose for all 300 historical visitors to this site over the past decade. But I have changed, the web has changed, and my knowledge base has grown to the point now where I want to self-manage and self-host a lot more.

To this purpose, I decided to write a very tiny blog project using [ASP.Net Core](https://dotnet.microsoft.com/learn/aspnet/what-is-aspnet-core). All it does really is go and parse some markdown files, containing the blog content, into the nice pages you see here. It's relatively simple compared to the Dodo project and the lion's share was done in a day.

I've also ended up with lots of tiny little services and projects - my blog, the Dodo project, and some little bits and pieces all scattered around in different hosting solutions. While spreading myself around has been valuable, for instance in learning a bit about deploying services on Azure, they are a bit expensive to maintain. So, now I have all my projects, services, and sites hosted on one machine behind an Apache reverse-proxy. Good fun setting up, but I understand why people prefer just serving static files. Perhaps this is an overly complicated choice for a simple blog that won't change very much, and I might look into some static page rendering in future. For now, the ASP.Net design vastly favours file reads over memory or processing footprint - basically zero objects are kept in memory outside of the request. Thanks Linq!

I also have been really disliking social media. Maybe I'm just a grumpy bastard but there's something a little nicer about writing for yourself, with no expectation of interaction or judgement from others. I'm hoping that the ease of contributing to this blog - just update a git repo! - will help me redirect some of my time and energy into introspection instead of argument.

Some goals for continuing development:

- [x] IO-centric resource usage, because hits are few and far between
- [x] Zero cookies, logging or other data gathering about user interaction
- [ ] Tag cloud
- [ ] Better SEO and archivability
- [ ] An RSS feed
- [ ] More markdown features
- [ ] Little easter eggs