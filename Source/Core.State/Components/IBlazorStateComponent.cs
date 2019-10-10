namespace Core.State
{
  using MediatR;

  /// <summary>
  /// Minimum implementation needed for CoreState to function
  /// </summary>
  /// <example>
  ///   public class CoreStateComponent : BlazorComponent,
  ///     ICoreStateComponent
  ///  {
  ///    [Inject] public IMediator Mediator { get; set; }
  ///    [Inject] public IStore Store { get; set; }
  ///    public void ReRender() => StateHasChanged();
  /// }
  /// </example>
  public interface ICoreStateComponent // TODO: evaluate if this interface is even needed
  {
    string Id { get; }
    IMediator Mediator { get; set; }
    IStore Store { get; set; }

    void ReRender();
  }
}