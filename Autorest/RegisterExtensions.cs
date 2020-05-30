using System;
using System.Linq;
using System.Reflection;
using Autorest;
using AutoRest.Domain;
using Autorest.Domain.Infrastructure;
using Autorest.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;


// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up AutoRest
    /// </summary>
    public static class AutoMapperRegister
    {

        public static IMvcBuilder AddAutoRest(this IMvcBuilder builder)
        {
            builder.Services.AddAutoRestDomain();
            builder.ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericControllerFeatureProvider()));
            builder.Services.Configure<MvcOptions>(c =>
                c.Conventions.Add(new GenericRouteInjection()));
            return builder;
        }

      
    }
}