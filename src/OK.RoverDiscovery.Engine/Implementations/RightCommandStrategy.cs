using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;
using System.Collections.Generic;

namespace OK.RoverDiscovery.Engine.Implementations
{
    public class RightCommandStrategy : ICommandStrategy
    {
        private readonly IDictionary<DirectionEnum, DirectionEnum> _directionCycleMap
            = new Dictionary<DirectionEnum, DirectionEnum>()
            {
                { DirectionEnum.N, DirectionEnum.E },
                { DirectionEnum.E, DirectionEnum.S },
                { DirectionEnum.S, DirectionEnum.W },
                { DirectionEnum.W, DirectionEnum.N }
            };

        public PositionModel CalculatePosition(PositionModel lastPosition)
        {
            // Maybe, decreasing lastPosition's direction value for find the next direction is more effective
            // But, when DirectionEnum values are changed, the logic will not work expectedly

            lastPosition.Direction = _directionCycleMap[lastPosition.Direction];

            return lastPosition;
        }
    }
}