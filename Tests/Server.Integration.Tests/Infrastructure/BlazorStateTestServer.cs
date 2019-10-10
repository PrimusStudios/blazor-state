namespace TestApp.Server.Integration.Tests.Infrastructure
{
  using Microsoft.AspNetCore;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.TestHost;

  public class CoreStateTestServer : TestServer
  {
    public CoreStateTestServer() : base(WebHostBuilder()) { }

    private static IWebHostBuilder WebHostBuilder() =>
      WebHost.CreateDefaultBuilder()
      .UseStartup<Startup>()
      .UseEnvironment("Local");
  }
}