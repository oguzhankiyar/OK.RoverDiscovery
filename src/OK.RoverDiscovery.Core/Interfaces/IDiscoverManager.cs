using OK.RoverDiscovery.Core.Models;

namespace OK.RoverDiscovery.Core.Interfaces
{
    public interface IDiscoverManager
    {
        DiscoverModel Discover(PlateauModel plateau, RoverModel rover);
    }
}