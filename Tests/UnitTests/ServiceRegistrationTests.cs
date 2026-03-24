using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Core.Services;

namespace UnitTests
{
    public class ServiceRegistrationTests
    {
        [Fact]
        public void AddCoreServices_Registers_TokenService_And_Repository()
        {
            var services = new ServiceCollection();
            services.AddCoreServices();

            var provider = services.BuildServiceProvider();

            var tokenService = provider.GetService<ITokenService>();
            var repo = provider.GetService<Core.IAppRepository>();

            Assert.NotNull(tokenService);
            Assert.NotNull(repo);
        }
    }
}
