using OK.RoverDiscovery.Core.Models;
using System.Collections.Generic;

namespace OK.RoverDiscovery.Core.Interfaces
{
    public interface ICommandParser
    {
        PlateauModel Plateau { get; }
        
        List<RoverModel> Rovers { get; }

        void Parse(string commandString);
    }
}