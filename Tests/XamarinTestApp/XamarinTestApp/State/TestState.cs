namespace XamarinTestApp.State
{
  public partial class TestState : BindableBaseState<TestState>  
  {
    string text;
    public string Text { get => text; private set => SetProperty(ref text, value); }


    protected override void Initialize()
    {

    }
  }
}
