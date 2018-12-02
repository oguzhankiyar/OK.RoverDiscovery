using System;

namespace OK.RoverDiscovery.Engine.Exceptions
{
    public class CommandParserException : Exception
    {
        public CommandParserException()
        {
        }

        public CommandParserException(string message) : base(message)
        {
        }
    }
}