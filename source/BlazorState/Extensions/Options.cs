namespace BlazorState
{
  using System.Collections.Generic;
  using System.Reflection;

  public class Options
  {
    ///// <summary>
    ///// Assemblies to be searched for MediatR Requests
    ///// </summary>
    public IEnumerable<Assembly> Assemblies { get; set; }

    public Options()
    {
      Assemblies = new Assembly[] { };
    }
  }
}