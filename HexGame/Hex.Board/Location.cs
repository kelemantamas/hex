using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Board
{
    public struct Location
    {
        private static readonly Location NullLocation = new Location(-1, -1);

        private readonly int x;
        private readonly int y;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Location Null
        {
            get { return NullLocation; }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }

        public static bool operator ==(Location location1, Location location2)
        {
            return location1.Equals(location2);
        }

        public static bool operator !=(Location location1, Location location2)
        {
            return !location1.Equals(location2);
        }

        public bool Equals(Location otherLocation)
        {
            return (this.X == otherLocation.X) && (this.Y == otherLocation.Y);
        }


        public override bool Equals(object otherObject)
        {
            if (otherObject == null)
            {
                return false;
            }

            if (this.GetType() != otherObject.GetType())
            {
                return false;
            }

            return this.Equals((Location)otherObject);
        }

        public override int GetHashCode()
        {
            const int HashXFactor = 1024;
            return (this.X * HashXFactor) + this.Y;
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", this.X, this.Y);
        }

        public bool IsNull()
        {
            return (this.X == -1) && (this.Y == -1);
        }
    }
}
