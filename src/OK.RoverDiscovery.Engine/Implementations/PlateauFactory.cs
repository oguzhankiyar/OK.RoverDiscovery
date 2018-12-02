using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;

namespace OK.RoverDiscovery.Engine.Implementations
{
    public class PlateauFactory : IPlateauFactory
    {
        private const int PlateauOriginX = 0;
        private const int PlateauOriginY = 0;

        public PlateauModel Create(CoordinateModel upperRightCoordinate)
        {
            return new PlateauModel()
            {
                Origin = new CoordinateModel(PlateauOriginX, PlateauOriginY),
                Width = upperRightCoordinate.X - PlateauOriginX + 1,
                Height = upperRightCoordinate.Y - PlateauOriginY + 1
            };
        }
    }
}