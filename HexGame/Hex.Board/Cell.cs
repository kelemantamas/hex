using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Board
{
    public class Cell
    {
        private Location location;
        public Cell(Location location)
        {
            this.location = location;
            this.IsOccupied = Occupied.Empty;
        }
        public Occupied IsOccupied { get; set; }

        public Location Location
        {
            get { return this.location; }
        }

        public int X
        {
            get { return this.location.X; }
        }

        public int Y
        {
            get { return this.location.Y; }
        }

        public bool IsEmpty()
        {
            return this.IsOccupied == Occupied.Empty;
        }

        public bool IsPlayer(bool playerX)
        {
            if (playerX)
            {
                return this.IsOccupied == Occupied.PlayerX;
            }

            return this.IsOccupied == Occupied.PlayerY;
        }

        public override string ToString()
        {
            return this.location + " " + OccupiedHelper.OccupiedToString(this.IsOccupied);
        }
    }
}
