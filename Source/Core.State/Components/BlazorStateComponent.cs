﻿namespace Core.State
{
  using System;
  using System.Collections.Concurrent;
  using MediatR;
  using Microsoft.AspNetCore.Components;

  /// <summary>
  /// A non required Base Class that injects Mediator and Store.
  /// And exposes StateHasChanged
  /// </summary>
  /// <remarks>Implements ICoreStateComponent by Injecting</remarks>
  public class CoreStateComponent : ComponentBase, IDisposable,
     ICoreStateComponent
  {
    static readonly ConcurrentDictionary<string, int> s_InstanceCounts = new ConcurrentDictionary<string, int>();

    public CoreStateComponent()
    {
      string name = GetType().Name;
      int count = s_InstanceCounts.AddOrUpdate(name, 1, (aKey, aValue) => aValue + 1);

      Id = $"{name}-{count}";
    }

    /// <summary>
    /// A generated unique Id based on the Class name and number of times they have been created
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Allows for the Assigning of a value one can use to select an element during automated testing.
    /// </summary>
    [Parameter] public string TestId { get; set; }

    [Inject] public IMediator Mediator { get; set; }
    [Inject] public IStore Store { get; set; }

    /// <summary>
    /// Maintains all components that subscribe to a State.
    /// Is updated by using the GetState method
    /// </summary>
    [Inject] public Subscriptions Subscriptions { get; set; }

    /// <summary>
    /// Exposes StateHasChanged
    /// </summary>
    public void ReRender()
    {
      base.InvokeAsync(StateHasChanged);
      OnStateChanged();
    }
   

    protected virtual void OnStateChanged()
    {

    }

    /// <summary>
    /// Place a Subscription for the calling component
    /// And returns the requested state
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetState<T>()
    {
      Type stateType = typeof(T);
      Subscriptions.Add(stateType, this);
      return Store.GetState<T>();
    }

    public void Dispose() => Subscriptions.Remove(this);
  }
}