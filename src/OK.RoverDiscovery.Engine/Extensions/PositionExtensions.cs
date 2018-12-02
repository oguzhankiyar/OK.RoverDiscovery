using OK.RoverDiscovery.Core.Models;

namespace OK.RoverDiscovery.Engine.Extensions
{
    public static class PositionExtensions
    {
        public static bool IsOutOfPlateauBound(this PositionModel position, PlateauModel plateau)
        {
            return position.Coordinate.X < plateau.Origin.X ||
                   position.Coordinate.Y < plateau.Origin.Y ||
                   position.Coordinate.X > plateau.Origin.X + plateau.Width ||
                   position.Coordinate.Y > plateau.Origin.Y + plateau.Height;
        }
    }
}