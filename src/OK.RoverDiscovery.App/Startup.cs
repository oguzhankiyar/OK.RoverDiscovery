using Microsoft.Extensions.DependencyInjection;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;
using OK.RoverDiscovery.Engine;
using OK.RoverDiscovery.Engine.Exceptions;
using System;
using System.Text;

namespace OK.RoverDiscovery.App
{
    public class Startup
    {
        public IServiceProvider Build()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddRoverDiscoveryImpl();

            return services.BuildServiceProvider();
        }

        public void Run(ICommandParser commandParser, IDiscoverManager discoverManager)
        {
            Console.WriteLine("Input:");

            string input = ReadMultiLine();

            try
            {
                StringBuilder output = new StringBuilder();

                commandParser.Parse(input);

                foreach (var rover in commandParser.Rovers)
                {
                    DiscoverModel discovery = discoverManager.Discover(commandParser.Plateau, rover);

                    output.AppendLine(string.Join(" ", discovery.LastPosition.Coordinate.X, discovery.LastPosition.Coordinate.Y, discovery.LastPosition.Direction));
                }

                Console.WriteLine("Output:");
                Console.WriteLine(output.ToString());
            }
            catch (CommandParserException ex)
            {
                Console.WriteLine("Command Parser Exception:");
                Console.WriteLine(ex.Message);
            }
            catch (CommandStrategyException ex)
            {
                Console.WriteLine("Command Strategy Exception:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }

        private string ReadMultiLine()
        {
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}