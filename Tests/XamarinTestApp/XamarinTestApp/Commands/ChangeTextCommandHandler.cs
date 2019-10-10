using Core.State;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace XamarinTestApp.State
{
  public partial class TestState
  {
    public class ChangeTextCommandHandler : ActionHandler<ChangeTextCommand>
    {
      public ChangeTextCommandHandler(IStore aStore) : base(aStore)
      {
      }

      private TestState State => Store.GetState<TestState>();
      public override Task<Unit> Handle(ChangeTextCommand aAction, CancellationToken aCancellationToken)
      {
        State.Text = aAction.Text;

        return Task.FromResult(Unit.Value);
      }
    }
  }
}
