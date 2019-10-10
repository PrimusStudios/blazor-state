namespace TestApp.Client.Features.Application
{
  using Core.State;

  internal partial class ApplicationState : State<ApplicationState>
  {
    public string Name { get; private set; }

    public string Version => GetType().Assembly.GetName().Version.ToString();

    public ApplicationState() { }

    protected override void Initialize() => Name = "Blazor State Demo Application";
  }
}