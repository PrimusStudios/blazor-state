namespace Core.State.Features.Routing
{
  using Core.State;

  /// <summary>
  /// Maintain the Route in Blazor-State
  /// </summary>
  public partial class RouteState : State<RouteState>
  {
    public string Route { get; private set; }

    protected override void Initialize() { }
  }
}