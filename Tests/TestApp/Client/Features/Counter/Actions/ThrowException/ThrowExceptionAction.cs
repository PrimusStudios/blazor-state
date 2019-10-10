namespace TestApp.Client.Features.Counter
{
  using Core.State;

  public partial class CounterState
  {
    public class ThrowExceptionAction : IAction
    {
      public string Message { get; set; }
    }
  }
}