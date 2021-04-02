using Eatigo.Eatilink.Api;
using Eatigo.Eatilink.Test.Unit.DependencyResolver;
using Eatigo.Eatilink.Validator;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Eatigo.Eatilink.Test.Unit.Security
{
    public class PayloadValidatorUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly string accessToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkZW50aXR5IjoiSGFzYW4iLCJuYmYiOjE2MTcyNzk1MDAsImV4cCI6MTYxOTg3MTUwMCwiaWF0IjoxNjE3Mjc5NTAwfQ.I0W_jbhcUUCb3kQBjg3GB0TiJn7WEwFRnUiN9PCI4Lg";

        public PayloadValidatorUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void EmptyOriginalUrl()
        {
            var linkShortenerValidator = _serviceProvider.GetService<ILinkShortenerValidator>();
            var (statusCode, errorResult) = linkShortenerValidator.PayloadValidator(accessToken, string.Empty);
            
            Assert.False(statusCode != StatusCodes.Status200OK);
        }

        [Fact]
        public void NotEmptyOriginalUrl()
        {
            var linkShortenerValidator = _serviceProvider.GetService<ILinkShortenerValidator>();
            var (statusCode, errorResult) = linkShortenerValidator.PayloadValidator(accessToken, "https://eatigo.com/th/bangkok/en");
            
            Assert.True(statusCode == StatusCodes.Status200OK);
        }

        [Fact]
        public void InvalidOriginalUrl()
        {
            var linkShortenerValidator = _serviceProvider.GetService<ILinkShortenerValidator>();
            var (statusCode, errorResult) = linkShortenerValidator.PayloadValidator(accessToken, "Hasan.https://eatigo.com/th/bangkok/en");
            
            Assert.False(statusCode != StatusCodes.Status200OK);
        }

        [Fact]
        public void ValidOriginalUrl()
        {
            var linkShortenerValidator = _serviceProvider.GetService<ILinkShortenerValidator>();
            var (statusCode, errorResult) = linkShortenerValidator.PayloadValidator(accessToken, "https://eatigo.com/th/bangkok/en");
            
            Assert.True(statusCode == StatusCodes.Status200OK);
        }
    }
}
