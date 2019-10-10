namespace TestApp.Client.Features.Counter
{
  using Core.State;

  public partial class CounterState
  {
    public class IncrementCounterAction : IAction
    {
      public int Amount { get; set; }
    }
  }
}