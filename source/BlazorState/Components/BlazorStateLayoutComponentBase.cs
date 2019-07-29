namespace BlazorState
{
  using Microsoft.AspNetCore.Components;
  public abstract class BlazorStateLayoutComponentBase : BlazorStateComponent
  {
    internal const string BodyPropertyName = nameof(Body);

    /// <summary>
    /// Gets the content to be rendered inside the layout.
    /// </summary>
    [Parameter]
    public RenderFragment Body { get; private set; }
  }
}
