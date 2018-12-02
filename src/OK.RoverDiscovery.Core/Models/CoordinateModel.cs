namespace OK.RoverDiscovery.Core.Models
{
    public class CoordinateModel
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public CoordinateModel(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            CoordinateModel coordinate = obj as CoordinateModel;

            return coordinate.X == X && coordinate.Y == Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }
}