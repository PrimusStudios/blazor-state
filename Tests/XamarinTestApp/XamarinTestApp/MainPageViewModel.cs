using BlazorState;
using BlazorState.Components;
using MediatR;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinTestApp.State;

namespace XamarinTestApp
{
  public class MainPageViewModel : BaseViewModel
  {

    public TestState TestState { get; }

    public ICommand HeyCommand => new Command(async () => await ChangeText("Hey"));

    public ICommand YoCommand => new Command(async () => await ChangeText("Yo"));

    public MainPageViewModel(IMediator mediator, IStore store, Subscriptions subs) : base(mediator, store, subs)
    {
      TestState = GetState<TestState>();
    }


    async Task ChangeText(string text)
    {
      await Mediator.Send(new ChangeTextCommand
      {
        Text = text
      });
    }

  }
}
