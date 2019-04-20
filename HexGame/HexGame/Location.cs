using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame
{
   public class Location
    {
        private static Location NullLocation = new Location(-1, -1);
        public int x { get; set; }
        public int y { get; set; }
        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public bool IsNull()
        {
            return (this.x == -1) && (this.y == -1);
        }
        public static Location Null
        {
            get { return NullLocation; }
        }
    }
}
