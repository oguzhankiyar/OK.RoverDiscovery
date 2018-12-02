using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;
using OK.RoverDiscovery.Engine.Implementations;
using System;
using Xunit;

namespace OK.RoverDiscovery.Engine.Tests
{
    public class CommandStrategyTests
    {
        private readonly ICommandStrategy _leftCommandStrategy;
        private readonly ICommandStrategy _rightCommandStrategy;
        private readonly ICommandStrategy _moveCommandStrategy;

        public CommandStrategyTests()
        {
            _leftCommandStrategy = new LeftCommandStrategy();
            _rightCommandStrategy = new RightCommandStrategy();
            _moveCommandStrategy = new MoveCommandStrategy();
        }

        private int GetRandomInteger() => new Random().Next(0, 100);

        [Fact]
        public void LeftCommandStrategy_ShouldRotateToWest_WhenDirectionIsNorth()
        {
            DirectionEnum direction = DirectionEnum.N;
            DirectionEnum expected = DirectionEnum.W;

            var actual = _leftCommandStrategy.CalculatePosition(new PositionModel() { Direction = direction });

            Assert.NotNull(actual);
            Assert.Equal((int)expected, (int)actual.Direction);
        }

        [Fact]
        public void LeftCommandStrategy_ShouldRotateToNorth_WhenDirectionIsEast()
        {
            DirectionEnum direction = DirectionEnum.E;
            DirectionEnum expected = DirectionEnum.N;

            var actual = _leftCommandStrategy.CalculatePosition(new PositionModel() { Direction = direction });

            Assert.NotNull(actual);
            Assert.Equal((int)expected, (int)actual.Direction);
        }

        [Fact]
        public void LeftCommandStrategy_ShouldRotateToEast_WhenDirectionIsSouth()
        {
            DirectionEnum direction = DirectionEnum.S;
            DirectionEnum expected = DirectionEnum.E;

            var actual = _leftCommandStrategy.CalculatePosition(new PositionModel() { Direction = direction });

            Assert.NotNull(actual);
            Assert.Equal((int)expected, (int)actual.Direction);
        }

        [Fact]
        public void LeftCommandStrategy_ShouldRotateToSouth_WhenDirectionIsWest()
        {
            DirectionEnum direction = DirectionEnum.W;
            DirectionEnum expected = DirectionEnum.S;

            var actual = _leftCommandStrategy.CalculatePosition(new PositionModel() { Direction = direction });

            Assert.NotNull(actual);
            Assert.Equal((int)expected, (int)actual.Direction);
        }

        [Fact]
        public void RightCommandStrategy_ShouldRotateToEast_WhenDirectionIsNorth()
        {
            DirectionEnum direction = DirectionEnum.N;
            DirectionEnum expected = DirectionEnum.E;

            var actual = _rightCommandStrategy.CalculatePosition(new PositionModel() { Direction = direction });

            Assert.NotNull(actual);
            Assert.Equal((int)expected, (int)actual.Direction);
        }

        [Fact]
        public void RightCommandStrategy_ShouldRotateToSouth_WhenDirectionIsEast()
        {
            DirectionEnum direction = DirectionEnum.E;
            DirectionEnum expected = DirectionEnum.S;

            var actual = _rightCommandStrategy.CalculatePosition(new PositionModel() { Direction = direction });

            Assert.NotNull(actual);
            Assert.Equal((int)expected, (int)actual.Direction);
        }

        [Fact]
        public void RightCommandStrategy_ShouldRotateToWest_WhenDirectionIsSouth()
        {
            DirectionEnum direction = DirectionEnum.S;
            DirectionEnum expected = DirectionEnum.W;

            var actual = _rightCommandStrategy.CalculatePosition(new PositionModel() { Direction = direction });

            Assert.NotNull(actual);
            Assert.Equal((int)expected, (int)actual.Direction);
        }

        [Fact]
        public void RightCommandStrategy_ShouldRotateToNorth_WhenDirectionIsWest()
        {
            DirectionEnum direction = DirectionEnum.W;
            DirectionEnum expected = DirectionEnum.N;

            var actual = _rightCommandStrategy.CalculatePosition(new PositionModel() { Direction = direction });

            Assert.NotNull(actual);
            Assert.Equal((int)expected, (int)actual.Direction);
        }

        [Fact]
        public void MoveCommandStrategy_ShouldIncreaseCoordinateY_WhenDirectionIsNorth()
        {
            DirectionEnum direction = DirectionEnum.N;
            int randomX = GetRandomInteger();
            int randomY = GetRandomInteger();
            CoordinateModel coordinate = new CoordinateModel(randomX, randomY);

            var actual = _moveCommandStrategy.CalculatePosition(new PositionModel() { Coordinate = coordinate, Direction = direction });

            var expected = new CoordinateModel(randomX, randomY + 1);

            Assert.NotNull(actual);
            Assert.Equal(actual.Coordinate, expected);
        }

        [Fact]
        public void MoveCommandStrategy_ShouldIncreaseCoordinateX_WhenDirectionIsEast()
        {
            DirectionEnum direction = DirectionEnum.E;
            int randomX = GetRandomInteger();
            int randomY = GetRandomInteger();
            CoordinateModel coordinate = new CoordinateModel(randomX, randomY);

            var actual = _moveCommandStrategy.CalculatePosition(new PositionModel() { Coordinate = coordinate, Direction = direction });

            var expected = new CoordinateModel(randomX + 1, randomY);

            Assert.NotNull(actual);
            Assert.Equal(actual.Coordinate, expected);
        }

        [Fact]
        public void MoveCommandStrategy_ShouldDecreaseCoordinateY_WhenDirectionIsSouth()
        {
            DirectionEnum direction = DirectionEnum.S;
            int randomX = GetRandomInteger();
            int randomY = GetRandomInteger();
            CoordinateModel coordinate = new CoordinateModel(randomX, randomY);

            var actual = _moveCommandStrategy.CalculatePosition(new PositionModel() { Coordinate = coordinate, Direction = direction });

            var expected = new CoordinateModel(randomX, randomY - 1);

            Assert.NotNull(actual);
            Assert.Equal(actual.Coordinate, expected);
        }

        [Fact]
        public void MoveCommandStrategy_ShouldDecreaseCoordinateX_WhenDirectionIsWest()
        {
            DirectionEnum direction = DirectionEnum.W;
            int randomX = GetRandomInteger();
            int randomY = GetRandomInteger();
            CoordinateModel coordinate = new CoordinateModel(randomX, randomY);

            var actual = _moveCommandStrategy.CalculatePosition(new PositionModel() { Coordinate = coordinate, Direction = direction });

            var expected = new CoordinateModel(randomX - 1, randomY);

            Assert.NotNull(actual);
            Assert.Equal(actual.Coordinate, expected);
        }
    }
}