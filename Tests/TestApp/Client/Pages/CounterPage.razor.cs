namespace TestApp.Client.Pages
{
  using Core.State.Features.Routing;
  using System.Threading.Tasks;
  using TestApp.Client.Features.Base.Components;

  public class CounterPageBase : BaseComponent
  {
    protected async Task ButtonClick() =>
      _ = await Mediator.Send(new RouteState.ChangeRouteAction { NewRoute = "/" });
  }
}