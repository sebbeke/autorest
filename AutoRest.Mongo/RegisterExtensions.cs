using System;
using AutoRest.Models;
using AutoRest.Mongo;
using MongoDB.Bson.Serialization.Conventions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up Mongo generic repository and mongo file manager
    /// </summary>
    public static class RegisterExtension
    {

        public static IServiceCollection AddAutoRestMongo(this IServiceCollection services, MongoConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var pack = new ConventionPack {new StringObjectIdConvention()};
            ConventionRegistry.Register("AutorestConventions", pack, _ => true);

            return services
                .AddSingleton<MongoConfiguration>(m => config)
                .AddScoped(typeof(IFileManager), typeof(MongoFileManager))
                .AddScoped(typeof(IGenericRepository<>), typeof(MongoGenericRepository<>));
        }
    }
}