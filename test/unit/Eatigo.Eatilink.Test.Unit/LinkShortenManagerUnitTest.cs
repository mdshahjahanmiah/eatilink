using Eatigo.Eatilink.Api;
using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Test.Unit.DependencyResolver;
using Eatigo.Eatilink.Validator;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Eatigo.Eatilink.Test.Unit.Security
{
    public class LinkShortenManagerUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        public LinkShortenManagerUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void Shorten()
        {
            var linkShortenManager = _serviceProvider.GetService<ILinkShortenManager>();

            ShortUrlRequest model = new ShortUrlRequest(){ OriginalUrl = "https://eatigo.com/th/bangkok/en" };
            var response = linkShortenManager.Shorten(model);
            Assert.True(response != null);
        }
    }
}
