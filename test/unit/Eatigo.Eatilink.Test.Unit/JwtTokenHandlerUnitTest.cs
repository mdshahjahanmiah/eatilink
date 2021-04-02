using Eatigo.Eatilink.Api;
using Eatigo.Eatilink.Security.Handlers;
using Eatigo.Eatilink.Test.Unit.DependencyResolver;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace Eatigo.Eatilink.Test.Unit.Security
{
    public class JwtTokenHandlerUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly string accessToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkZW50aXR5IjoiSGFzYW4iLCJuYmYiOjE2MTcyNzk1MDAsImV4cCI6MTYxOTg3MTUwMCwiaWF0IjoxNjE3Mjc5NTAwfQ.I0W_jbhcUUCb3kQBjg3GB0TiJn7WEwFRnUiN9PCI4Lg";

        public JwtTokenHandlerUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void GenerateAccessToekn()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.GenerateJwtSecurityToken("Hasan");
            Assert.NotEmpty(token);
        }

        [Fact]
        public void PrepareTokenFromAccessToekn()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void VerifyJwtSecurityToken()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
            var (isVerified, userId) = jwtTokenHandler.VerifyJwtSecurityToken(token);
            Assert.True(isVerified || !string.IsNullOrEmpty(userId));
        }
    }
}
