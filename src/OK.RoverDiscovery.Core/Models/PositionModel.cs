using OK.RoverDiscovery.Core.Enums;

namespace OK.RoverDiscovery.Core.Models
{
    public class PositionModel
    {
        public CoordinateModel Coordinate { get; set; }

        public DirectionEnum Direction { get; set; }
    }
}