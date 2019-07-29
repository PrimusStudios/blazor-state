﻿namespace BlazorState
{
  using BlazorState.Features.JavaScriptInterop;
  using BlazorState.Pipeline.State;
  using BlazorState.Services;
  using MediatR;
  using MediatR.Pipeline;
  using Microsoft.AspNetCore.Components;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Logging.Abstractions;
  using System;
  using System.Linq;
  using System.Net.Http;

  public static class ServiceCollectionExtensions
  {
    /// <summary>
    /// Register BlazorState services based on the aConfigure options
    /// </summary>
    /// <param name="aServices"></param>
    /// <param name="aConfigure"></param>
    /// <returns></returns>
    /// <example></example>
    /// <remarks>The order of registration matters.
    /// If the user wants to change they can configure themselves vs using this extension</remarks>
    public static IServiceCollection AddBlazorState(
      this IServiceCollection aServices,
      Action<Options> aConfigure = null)
    {
      ServiceDescriptor flagServiceDescriptor = aServices.FirstOrDefault(
        aServiceDescriptor => aServiceDescriptor.ServiceType == typeof(BlazorHostingLocation));

      if (flagServiceDescriptor == null)
      {
        var options = new Options();
        aConfigure?.Invoke(options);

        EnsureLogger(aServices);
        EnsureHttpClient(aServices);
        EnsureMediator(aServices, options);

        aServices.AddScoped<BlazorHostingLocation>();
        aServices.AddScoped<JsonRequestHandler>();
        aServices.AddScoped<Subscriptions>();
        aServices.AddScoped(typeof(IRequestPostProcessor<,>), typeof(RenderSubscriptionsPostProcessor<,>));
        aServices.AddScoped<IStore, Store>();
      }
      return aServices;
    }

    private static void EnsureHttpClient(IServiceCollection aServices)
    {
      var blazorHostingLocation = new BlazorHostingLocation();

      // Server Side Blazor doesn't register HttpClient by default
      if (blazorHostingLocation.IsServerSide)
      {
        // Double check that nothing is registered.
        if (!aServices.Any(aServiceDescriptor => aServiceDescriptor.ServiceType == typeof(HttpClient)))
        {
          // Setup HttpClient for server side in a client side compatible fashion
          aServices.AddScoped<HttpClient>(aServiceProvider =>
          {
            // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
            IUriHelper uriHelper = aServiceProvider.GetRequiredService<IUriHelper>();
            return new HttpClient
            {
              BaseAddress = new Uri(uriHelper.GetBaseUri())
            };
          });
        }
      }
    }

    /// <summary>
    /// If no ILogger is registered it would throw as we inject it.  This provides us with a NullLogger to avoid that
    /// </summary>
    /// <param name="aServices"></param>
    private static void EnsureLogger(IServiceCollection aServices)
    {
      ServiceDescriptor loggerServiceDescriptor = aServices.FirstOrDefault(
        aServiceDescriptor => aServiceDescriptor.ServiceType == typeof(ILogger<>));

      if (loggerServiceDescriptor == null)
      {
        aServices.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
      }
    }

    /// <summary>
    /// Scan Assemblies for Handlers.
    /// </summary>
    /// <param name="aServices"></param>
    /// <param name="aOptions"></param>
    /// <param name="aCallingAssembly">The calling assembly</param>
    private static void EnsureMediator(IServiceCollection aServices, Options aOptions)
    {
      ServiceDescriptor mediatorServiceDescriptor = aServices.FirstOrDefault(
        aServiceDescriptor => aServiceDescriptor.ServiceType == typeof(IMediator));

      if (mediatorServiceDescriptor == null)
      {
        aServices.AddMediatR(aOptions.Assemblies.ToArray());
      }
    }

  }
}