﻿using OK.RoverDiscovery.Core.Models;

namespace OK.RoverDiscovery.Core.Interfaces
{
    public interface ICommandStrategy
    {
        PositionModel CalculatePosition(PositionModel lastPosition);
    }
}