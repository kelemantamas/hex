using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame
{
    public class Cell
    {
        private Location location;
        public Location Location
        {
            get;
            set;
        }
        public Occupied IsOccupied { get; set; }

        public Cell(Location location)
        {
            this.location = location;
            this.IsOccupied = Occupied.Empty;
        }

        public Cell(Cell originalCell)
        {
            if (originalCell != null)
            {
                this.location = originalCell.location;
                this.IsOccupied = originalCell.IsOccupied;
            }
        }

        public int x
        {
            get { return location.x; }
        }
        public int y
        {
            get { return location.y; }
        }
    }
}