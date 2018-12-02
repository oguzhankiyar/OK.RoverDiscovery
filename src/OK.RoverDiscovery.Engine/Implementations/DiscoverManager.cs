using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;
using OK.RoverDiscovery.Engine.Exceptions;
using OK.RoverDiscovery.Engine.Extensions;
using System;
using System.Collections.Generic;

namespace OK.RoverDiscovery.Engine.Implementations
{
    public class DiscoverManager : IDiscoverManager
    {
        private readonly IDictionary<CommandEnum, ICommandStrategy> _strategies;

        public DiscoverManager(IDictionary<CommandEnum, ICommandStrategy> strategies)
        {
            _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
        }

        public DiscoverModel Discover(PlateauModel plateau, RoverModel rover)
        {
            PositionModel lastPosition = new PositionModel()
            {
                Coordinate = new CoordinateModel(rover.Position.Coordinate.X, rover.Position.Coordinate.Y),
                Direction = rover.Position.Direction
            };

            foreach (var command in rover.Commands)
            {
                if (!_strategies.TryGetValue(command, out ICommandStrategy commandStrategy))
                {
                    throw new CommandStrategyException($"Invalid Strategy: {command}");
                }

                lastPosition = commandStrategy.CalculatePosition(lastPosition);

                if (lastPosition.IsOutOfPlateauBound(plateau))
                {
                    throw new CommandStrategyException($"Out Of Bound: ({lastPosition.Coordinate.X}, {lastPosition.Coordinate.Y})");
                }
            }

            return new DiscoverModel()
            {
                FirstPosition = rover.Position,
                LastPosition = lastPosition
            };
        }
    }
}