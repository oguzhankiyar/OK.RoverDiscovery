using OK.RoverDiscovery.Core.Models;

namespace OK.RoverDiscovery.Core.Interfaces
{
    public interface IPlateauFactory
    {
        PlateauModel Create(CoordinateModel upperRightCoordinate);
    }
}