using System;
using System.Linq;

namespace OK.RoverDiscovery.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();

            IServiceProvider serviceProvider = startup.Build();

            var method = startup.GetType()
                                .GetMethods()
                                .FirstOrDefault(x => x.Name == nameof(Startup.Run));

            var parameters = method.GetParameters()
                                   .Select(p => serviceProvider.GetService(p.ParameterType))
                                   .ToArray();

            method.Invoke(startup, parameters);
        }
    }
}