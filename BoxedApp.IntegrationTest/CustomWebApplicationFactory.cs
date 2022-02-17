using BoxedApp.Options;
using BoxedApp.Repositories;
using BoxedApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http;

namespace BoxedApp.IntegrationTest
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
    {
        public CustomWebApplicationFactory()
        {
            this.ClientOptions.AllowAutoRedirect = false;
        }

        public ApplicationOptions ApplicationOptions { get; private set; } = default!;


        public Mock<IClockService> ClockServiceMock { get; } = new Mock<IClockService>(MockBehavior.Strict);

        public Mock<IBookRepository> BookRepositoryMock { get; } = new Mock<IBookRepository>(MockBehavior.Strict);

        //public void VerifyAllMocks() => Mock.VerifyAll(this.BookRepositoryMock, this.ClockServiceMock);

        protected override void ConfigureClient(HttpClient client)
        {
            using (var serviceScope = this.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                this.ApplicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();
            }

            base.ConfigureClient(client);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder
                .UseEnvironment(Constants.EnvironmentName.Test)
                .ConfigureServices(this.ConfigureServices);

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        this.VerifyAllMocks();
        //    }

        //    base.Dispose(disposing);
        //}
    }
}
