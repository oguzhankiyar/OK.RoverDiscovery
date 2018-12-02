using System;

namespace OK.RoverDiscovery.Engine.Exceptions
{
    public class CommandStrategyException : Exception
    {
        public CommandStrategyException()
        {
        }

        public CommandStrategyException(string message) : base(message)
        {
        }
    }
}