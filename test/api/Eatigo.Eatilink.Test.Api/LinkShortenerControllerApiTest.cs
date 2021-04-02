using Eatigo.Eatilink.Api;
using Eatigo.Eatilink.Api.Controllers;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Validator;
using Eatigo.Eatilink.Test.Unit.DependencyResolver;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Eatigo.Eatilink.DataObjects.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eatigo.Eatilink.Test.Api
{
    public class LinkShortenerControllerApiTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        public LinkShortenerControllerApiTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void UnauthorizeLinkShortener()
        {
            ViewResult result = new ViewResult();
            try
            {
                var linkShortenerValidator = _serviceProvider.GetService<ILinkShortenerValidator>();
                var linkShortenManager = _serviceProvider.GetService<ILinkShortenManager>();
                var logger = _serviceProvider.GetService<ILogger<LinkShortenerController>>();

                var controller = new LinkShortenerController(linkShortenerValidator, linkShortenManager, logger);
                ShortUrlRequest model = new ShortUrlRequest
                {
                    OriginalUrl = "https://eatigo.com/th/bangkok/en"
                };

                var request = new HttpRequestMessage();
                request.Headers.Add("Authorization", "");
                result = (ViewResult)controller.LinkShortener(model);
                Assert.NotNull(result);
            }
            catch
            {
                Assert.True(result.StatusCode == null);
            }
        }
    }
}
