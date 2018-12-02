using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;
using OK.RoverDiscovery.Engine.Implementations;
using Xunit;

namespace OK.RoverDiscovery.Engine.Tests
{
    public class PlateauFactoryTests
    {
        private readonly IPlateauFactory _plateauFactory;

        public PlateauFactoryTests()
        {
            _plateauFactory = new PlateauFactory();
        }

        [Fact]
        public void Create_ShouldReturnValidPlateau()
        {
            var actual = _plateauFactory.Create(new CoordinateModel(5, 5));

            Assert.NotNull(actual);
            Assert.NotNull(actual.Origin);
            Assert.Equal(0, actual.Origin.X);
            Assert.Equal(0, actual.Origin.Y);
            Assert.Equal(6, actual.Width);
            Assert.Equal(6, actual.Height);
        }
    }
}