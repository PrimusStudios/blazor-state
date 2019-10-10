namespace TestApp.Server.Integration.Tests.Infrastructure
{
  using System;

  public class TestFixture
  {
    private readonly CoreStateTestServer CoreStateTestServer;

    /// <summary>
    /// This is the ServiceProvider that will be used by the Server
    /// </summary>
    public IServiceProvider ServiceProvider => CoreStateTestServer.Services;

    public TestFixture(CoreStateTestServer aCoreStateTestServer)
    {
      CoreStateTestServer = aCoreStateTestServer;
    }
  }
}