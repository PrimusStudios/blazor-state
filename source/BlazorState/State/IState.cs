namespace BlazorState
{
  using System;
  using System.Collections.Generic;

  public interface IState
  {
    Guid Guid { get; }
  }

  public interface IState<TState> : IState
  {
    TState State { get; }
  }
}