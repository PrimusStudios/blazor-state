namespace Core.State
{
  using Microsoft.Extensions.Logging;
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class Subscriptions
  {
    private readonly ILogger Logger;

    private readonly List<Subscription> CoreStateComponentReferencesList;

    public Subscriptions(ILogger<Subscriptions> aLogger)
    {
      Logger = aLogger;
      CoreStateComponentReferencesList = new List<Subscription>();
    }

    public Subscriptions Add<T>(ICoreStateComponent aCoreStateComponent)
    {
      Type type = typeof(T);

      return Add(type, aCoreStateComponent);
    }

    public Subscriptions Add(Type aType, ICoreStateComponent aCoreStateComponent)
    {
      // Add only once.
      if (!CoreStateComponentReferencesList.Any(aSubscription => aSubscription.StateType == aType && aSubscription.ComponentId == aCoreStateComponent.Id))
      {
        var subscription = new Subscription(
          aType,
          aCoreStateComponent.Id,
          new WeakReference<ICoreStateComponent>(aCoreStateComponent));

        CoreStateComponentReferencesList.Add(subscription);
      }

      return this;
    }

    public override bool Equals(object obj) => obj is Subscriptions subscriptions && EqualityComparer<ILogger>.Default.Equals(Logger, subscriptions.Logger) && EqualityComparer<List<Subscription>>.Default.Equals(CoreStateComponentReferencesList, subscriptions.CoreStateComponentReferencesList);

    public override int GetHashCode()
    {
      var hashCode = -914156548;
      hashCode = hashCode * -1521134295 + EqualityComparer<ILogger>.Default.GetHashCode(Logger);
      hashCode = hashCode * -1521134295 + EqualityComparer<List<Subscription>>.Default.GetHashCode(CoreStateComponentReferencesList);
      return hashCode;
    }

    public Subscriptions Remove(ICoreStateComponent aCoreStateComponent)
    {
      Logger.LogDebug($"Removing Subscription for {aCoreStateComponent.Id}");
      CoreStateComponentReferencesList.RemoveAll(aRecord => aRecord.ComponentId == aCoreStateComponent.Id);

      return this;
    }

    /// <summary>
    /// Will iterate over all subscriptions for the given type and call ReRender on each.
    /// If the target component no longer exists it will remove its subscription.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ReRenderSubscribers<T>()
    {
      Type type = typeof(T);

      ReRenderSubscribers(type);
    }

    /// <summary>
    /// Will iterate over all subscriptions for the given type and call ReRender on each.
    /// If the target component no longer exists it will remove its subscription.
    /// </summary>
    /// <param name="aType"></param>
    public void ReRenderSubscribers(Type aType)
    {
      IEnumerable<Subscription> subscriptions = CoreStateComponentReferencesList.Where(aRecord => aRecord.StateType == aType);
      foreach (Subscription subscription in subscriptions.ToList())
      {
        if (subscription.CoreStateComponentReference.TryGetTarget(out ICoreStateComponent target))
        {
          Logger.LogDebug($"ReRender ComponentId:{subscription.ComponentId} StateType.Name:{subscription.StateType.Name}");
          target.ReRender();
        }
        else
        {
          // If Dispose is called will I ever have items in this list that got Garbage collected?
          // Maybe for those that don't inherit from our BaseComponent?
          Logger.LogDebug($"Removing Subscription for ComponentId:{subscription.ComponentId} StateType.Name:{subscription.StateType.Name}");
          CoreStateComponentReferencesList.Remove(subscription);
        }
      }
    }

    private readonly struct Subscription : IEquatable<Subscription>
    {
      public WeakReference<ICoreStateComponent> CoreStateComponentReference { get; }

      public string ComponentId { get; }

      public Type StateType { get; }

      public Subscription(Type aStateType, string aComponentId, WeakReference<ICoreStateComponent> aCoreStateComponentReference)
      {
        StateType = aStateType;
        ComponentId = aComponentId;
        CoreStateComponentReference = aCoreStateComponentReference;
      }

      public static bool operator !=(Subscription aLeftSubscription, Subscription aRightSubscription) => !(aLeftSubscription == aRightSubscription);

      public static bool operator ==(Subscription aLeftSubscription, Subscription aRightSubscription) => aLeftSubscription.Equals(aRightSubscription);

      public bool Equals(Subscription aSubscription) =>
                    EqualityComparer<Type>.Default.Equals(StateType, aSubscription.StateType) &&
        ComponentId == aSubscription.ComponentId &&
        EqualityComparer<WeakReference<ICoreStateComponent>>.Default.Equals(CoreStateComponentReference, aSubscription.CoreStateComponentReference);

      public override bool Equals(object aObject) => this.Equals((Subscription)aObject);

      public override int GetHashCode() => ComponentId.GetHashCode();
    }
  }
}