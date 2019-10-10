﻿using Core.State;
using Core.State.Components;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinTestApp
{
  public abstract class BaseViewModel : CoreStateViewModel, INotifyPropertyChanged 
  {
    public BaseViewModel(IMediator mediator, IStore store, Subscriptions subs) : base(mediator, store, subs)
    {
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

      storage = value;
      RaisePropertyChanged(propertyName);

      return true;
    }


    protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
    {
      if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

      storage = value;
      onChanged?.Invoke();
      RaisePropertyChanged(propertyName);

      return true;
    }

    protected void RaisePropertyChanged([CallerMemberName]string propertyName = null) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));


    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) => PropertyChanged?.Invoke(this, args);
  }
}
