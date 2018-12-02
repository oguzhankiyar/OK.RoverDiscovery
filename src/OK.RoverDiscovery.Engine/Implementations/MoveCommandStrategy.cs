using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;

namespace OK.RoverDiscovery.Engine.Implementations
{
    public class MoveCommandStrategy : ICommandStrategy
    {
        public PositionModel CalculatePosition(PositionModel lastPosition)
        {
            switch (lastPosition.Direction)
            {
                case DirectionEnum.W:
                    lastPosition.Coordinate = new CoordinateModel(lastPosition.Coordinate.X - 1, lastPosition.Coordinate.Y);
                    break;
                case DirectionEnum.N:
                    lastPosition.Coordinate = new CoordinateModel(lastPosition.Coordinate.X, lastPosition.Coordinate.Y + 1);
                    break;
                case DirectionEnum.E:
                    lastPosition.Coordinate = new CoordinateModel(lastPosition.Coordinate.X + 1, lastPosition.Coordinate.Y);
                    break;
                case DirectionEnum.S:
                    lastPosition.Coordinate = new CoordinateModel(lastPosition.Coordinate.X, lastPosition.Coordinate.Y - 1);
                    break;
            }

            return lastPosition;
        }
    }
}