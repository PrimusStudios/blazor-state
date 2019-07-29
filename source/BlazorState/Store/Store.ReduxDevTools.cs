namespace BlazorState
{
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// The portion of the store that is only needed to support
  /// ReduxDevTools Integration
  /// </summary>
  public partial class Store : IReduxDevToolsStore
  {
    /// <summary>
    /// Returns the States in a manner that can be serialized
    /// </summary>
    /// <returns></returns>
    /// <remarks>Used only for ReduxDevTools</remarks>
    public IDictionary<string, object> GetSerializableState()
    {
      var states = new Dictionary<string, object>();
      foreach (KeyValuePair<string, IState> pair in States.OrderBy(aKeyValuePair => aKeyValuePair.Key))
      {
        states[pair.Key] = pair.Value;
      }

      return states;
    }
  }
}