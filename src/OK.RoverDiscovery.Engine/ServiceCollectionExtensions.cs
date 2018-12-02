using Microsoft.Extensions.DependencyInjection;
using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Engine.Implementations;
using System.Collections.Generic;

namespace OK.RoverDiscovery.Engine
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRoverDiscoveryImpl(this IServiceCollection services)
        {
            services.AddTransient<ICommandParser, CommandParser>();
            services.AddTransient<IDiscoverManager, DiscoverManager>();

            services.AddSingleton<IPlateauFactory, PlateauFactory>();
            services.AddSingleton<IDictionary<CommandEnum, ICommandStrategy>>(_ =>
                new Dictionary<CommandEnum, ICommandStrategy>()
                {
                    { CommandEnum.L, new LeftCommandStrategy() },
                    { CommandEnum.R, new RightCommandStrategy() },
                    { CommandEnum.M, new MoveCommandStrategy() }
                });

            return services;
        }
    }
}