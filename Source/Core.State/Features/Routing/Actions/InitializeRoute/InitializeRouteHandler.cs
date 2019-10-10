namespace Core.State.Features.Routing
{
  using System.Threading;
  using System.Threading.Tasks;
  using Core.State;
  using MediatR;
  using Microsoft.AspNetCore.Components;

  public partial class RouteState
  {
    internal class InitializeRouteHandler : ActionHandler<InitializeRouteAction>
    {
      public InitializeRouteHandler
      (
        IStore aStore,
        NavigationManager aNavigationManager
      ) : base(aStore)
      {
        NavigationManager = aNavigationManager;
      }

      private RouteState RouteState => Store.GetState<RouteState>();

      private readonly NavigationManager NavigationManager;

      public override Task<Unit> Handle(InitializeRouteAction aInitializeRouteRequest, CancellationToken aCancellationToken)
      {
        RouteState.Route = NavigationManager.Uri;
        return Unit.Task;
      }
    }
  }
}