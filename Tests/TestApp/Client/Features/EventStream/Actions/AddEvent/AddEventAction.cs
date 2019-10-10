namespace TestApp.Client.Features.EventStream
{
  using Core.State;

  internal partial class EventStreamState
  {
    public class AddEventAction : IAction
    {
      public string Message { get; set; }
    }
  }
}