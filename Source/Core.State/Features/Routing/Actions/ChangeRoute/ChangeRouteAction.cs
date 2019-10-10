namespace Core.State.Features.Routing
{
  using Core.State;

  public partial class RouteState
  {
    public class ChangeRouteAction : IAction
    {
      public string NewRoute { get; set; }
    }
  }
}