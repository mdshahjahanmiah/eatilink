using Eatigo.Eatilink.Api;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Test.Unit.DependencyResolver;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace Eatigo.Eatilink.Test.Unit.Security
{
    public class LinkShortenerServiceUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        public LinkShortenerServiceUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void GenerateShortUrl()
        {
            var linkShortenService = _serviceProvider.GetService<ILinkShortenService>();
            var token = linkShortenService.GenerateShortUrl();
            Assert.NotEmpty(token);
        }

        [Fact]
        public void ShortUrlToId()
        {
            var linkShortenService = _serviceProvider.GetService<ILinkShortenService>();
            var token = linkShortenService.GenerateShortUrl();
            var result = linkShortenService.ShortUrlToId(token);
            Assert.NotEmpty(result.ToString());
        }

        [Fact]
        public void IdToBase62()
        {
            var linkShortenService = _serviceProvider.GetService<ILinkShortenService>();
            var token = linkShortenService.GenerateShortUrl();
            var id = linkShortenService.ShortUrlToId(token);
            var result = linkShortenService.IdToBase62(id);
            Assert.NotEmpty(result);
        }
    }
}
