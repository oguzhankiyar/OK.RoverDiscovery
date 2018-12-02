using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Models;
using OK.RoverDiscovery.Engine.Extensions;
using Xunit;

namespace OK.RoverDiscovery.Engine.Tests
{
    public class PositionExtensionTests
    {
        [Fact]
        public void IsOutOfPlateauBound_ShouldReturnFalse_WhenPositionIsInInside()
        {
            PositionModel position = new PositionModel()
            {
                Coordinate = new CoordinateModel(2, 3),
                Direction = DirectionEnum.N
            };

            PlateauModel plateau = new PlateauModel()
            {
                Origin = new CoordinateModel(0, 0),
                Width = 6,
                Height = 6
            };

            var actual = PositionExtensions.IsOutOfPlateauBound(position, plateau);

            Assert.False(actual);
        }

        [Fact]
        public void IsOutOfPlateauBound_ShouldReturnTrue_WhenPositionIsInOutside()
        {
            PositionModel position = new PositionModel()
            {
                Coordinate = new CoordinateModel(2, 7),
                Direction = DirectionEnum.N
            };

            PlateauModel plateau = new PlateauModel()
            {
                Origin = new CoordinateModel(0, 0),
                Width = 6,
                Height = 6
            };

            var actual = PositionExtensions.IsOutOfPlateauBound(position, plateau);

            Assert.True(actual);
        }
    }
}