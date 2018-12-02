using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;
using OK.RoverDiscovery.Engine.Exceptions;
using OK.RoverDiscovery.Engine.Implementations;
using System.Collections.Generic;
using Xunit;

namespace OK.RoverDiscovery.Engine.Tests
{
    public class DiscoverManagerTests
    {
        private PlateauModel plateau = new PlateauModel()
        {
            Origin = new CoordinateModel(0, 0),
            Width = 6,
            Height = 6
        };

        private RoverModel rover = new RoverModel()
        {
            Commands = new List<CommandEnum>()
            {
                CommandEnum.L,
                CommandEnum.M,
                CommandEnum.L,
                CommandEnum.M,
                CommandEnum.L,
                CommandEnum.M,
                CommandEnum.L,
                CommandEnum.M,
                CommandEnum.M,
            },
            Position = new PositionModel()
            {
                Coordinate = new CoordinateModel(1, 2),
                Direction = DirectionEnum.N
            }
        };

        private RoverModel roverForOutOfBound = new RoverModel()
        {
            Commands = new List<CommandEnum>()
            {
                CommandEnum.L,
                CommandEnum.M,
                CommandEnum.L,
                CommandEnum.M,
                CommandEnum.L,
                CommandEnum.M,
                CommandEnum.L,
                CommandEnum.M,
                CommandEnum.M,
            },
            Position = new PositionModel()
            {
                Coordinate = new CoordinateModel(0, 0),
                Direction = DirectionEnum.N
            }
        };

        IDictionary<CommandEnum, ICommandStrategy> validStrategies = new Dictionary<CommandEnum, ICommandStrategy>()
                {
                    { CommandEnum.L, new LeftCommandStrategy() },
                    { CommandEnum.R, new RightCommandStrategy() },
                    { CommandEnum.M, new MoveCommandStrategy() }
                };

        [Fact]
        public void Discover_ShouldThrowException_WhenStrategyIsNotValid()
        {
            IDictionary<CommandEnum, ICommandStrategy> strategies = new Dictionary<CommandEnum, ICommandStrategy>();
            IDiscoverManager discoverManager = new DiscoverManager(strategies);

            Assert.Throws<CommandStrategyException>(() => discoverManager.Discover(plateau, rover));
        }

        [Fact]
        public void Discover_ShouldThrowException_WhenPositionIsOutOfBound()
        {
            IDiscoverManager discoverManager = new DiscoverManager(validStrategies);

            Assert.Throws<CommandStrategyException>(() => discoverManager.Discover(plateau, roverForOutOfBound));
        }

        [Fact]
        public void Discover_ShouldReturnDiscoverModel()
        {
            IDiscoverManager discoverManager = new DiscoverManager(validStrategies);

            var actual = discoverManager.Discover(plateau, rover);

            Assert.NotNull(actual);
            Assert.NotNull(actual.FirstPosition);
            Assert.NotNull(actual.FirstPosition.Coordinate);
            Assert.Equal(1, actual.FirstPosition.Coordinate.X);
            Assert.Equal(2, actual.FirstPosition.Coordinate.Y);
            Assert.Equal(DirectionEnum.N, actual.FirstPosition.Direction);

            Assert.NotNull(actual.LastPosition);
            Assert.NotNull(actual.LastPosition.Coordinate);
            Assert.Equal(1, actual.LastPosition.Coordinate.X);
            Assert.Equal(3, actual.LastPosition.Coordinate.Y);
            Assert.Equal(DirectionEnum.N, actual.FirstPosition.Direction);
        }
    }
}
