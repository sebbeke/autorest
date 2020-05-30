using System;
using Autorest.Domain.Infrastructure;
using Autorest.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AutoRest.Domain
{
    /// <summary>
    /// Extension methods for setting up AutoRest
    /// </summary>
    public static class AutoMapperRegister
    {

        public static IServiceCollection AddAutoRestDomain(this IServiceCollection services)
        {
            return services
                .AddSingleton(typeof(IRepositoryObservable<>), typeof(RepositoryObservable<>))
                .AddScoped(typeof(IAutoRestRepository<>), typeof(AutoRestrepository<>));
        }
    }
}