namespace TestApp.Client.Components
{
  using Core.State;
  using Core.State.Services;
  using Microsoft.AspNetCore.Components;

  public class BlazorLocationBase : CoreStateComponent
  {
    [Inject] public BlazorHostingLocation BlazorHostingLocation { get; set; }

    public string LocationName => BlazorHostingLocation.IsClientSide ? "Client Side" : "Server Side";
  }
}
