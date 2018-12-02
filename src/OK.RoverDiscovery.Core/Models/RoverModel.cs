using OK.RoverDiscovery.Core.Enums;
using System.Collections.Generic;

namespace OK.RoverDiscovery.Core.Models
{
    public class RoverModel
    {
        public PositionModel Position { get; set; }

        public List<CommandEnum> Commands { get; set; }
    }
}