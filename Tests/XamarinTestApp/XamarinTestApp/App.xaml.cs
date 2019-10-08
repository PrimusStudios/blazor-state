
using Autofac;
using BlazorState;
using BlazorState.Features.JavaScriptInterop;
using BlazorState.Pipeline.State;
using BlazorState.Services;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTestApp;
using XamarinTestApp.State;

namespace XamarinTestApp
{
  public partial class App : Application
  {
    public static IContainer Container { get; private set; }

    public App()
    {

      Container = CreateContainer();

      InitializeComponent();

      MainPage = new MainPage();
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }


    protected IContainer CreateContainer()
    {
      var builder = new ContainerBuilder();
      builder.AddMediatR(new[] { typeof(IMediator).Assembly, typeof(TestState).Assembly, typeof(ChangeTextCommand).Assembly });

      builder.RegisterGeneric(typeof(NullLogger<>))
      .As(typeof(ILogger<>))
      .SingleInstance();

      builder.RegisterGeneric(typeof(RenderSubscriptionsPostProcessor<,>))
          .As(typeof(IRequestPostProcessor<,>)).InstancePerLifetimeScope();
      //builder.RegisterGeneric(typeof(RequestValidationBehavior<,>))
      //    .As(typeof(IPipelineBehavior<,>)).InstancePerLifetimeScope();

      builder.RegisterType<ServiceProvider>().AsImplementedInterfaces();
      builder.RegisterType(typeof(BlazorHostingLocation)).InstancePerLifetimeScope();
      builder.RegisterType(typeof(JsonRequestHandler)).InstancePerLifetimeScope();
      builder.RegisterType(typeof(Subscriptions)).InstancePerLifetimeScope();

      builder.RegisterType<Store>().AsImplementedInterfaces().InstancePerLifetimeScope();

      builder.RegisterType<MainPageViewModel>();
      //var mediatrOpenTypes = new[]
      //{
      //          typeof(IValidator<>),
      //      };

      //foreach (var mediatrOpenType in mediatrOpenTypes)
      //{
      //  builder
      //      .RegisterAssemblyTypes(typeof(UserState).GetTypeInfo().Assembly, typeof(SyncSitesCommand).Assembly)
      //      .AsClosedTypesOf(mediatrOpenType)
      //      .AsImplementedInterfaces();
      //}

      return builder.Build();
    }
  }
}

public class ServiceProvider : IServiceProvider
{
  public object GetService(Type serviceType)
  {
    return App.Container.Resolve(serviceType);
  }
}


