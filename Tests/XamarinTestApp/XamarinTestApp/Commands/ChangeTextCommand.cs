using BlazorState;

namespace XamarinTestApp.State
{
  public class ChangeTextCommand : IAction
  {

    public string Text { get; set; }
    public ChangeTextCommand()
    {
      Text = string.Empty;
    }
  }
}
