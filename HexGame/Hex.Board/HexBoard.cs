using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Board
{
    using System;
    using System.Collections.Generic;
   
    public class HexBoard
    {
        private Cell[,] cells;
        private int size;
        private int movesPlayedCount;

        private Cell[,][] neighbours;
        private Cell[,][][] neighbours2;
        private Cell[,][] playerXBetweenEdge;
        private Cell[,][] playerYBetweenEdge;
        public HexBoard(int size)
        {
            this.InitCells(size);
        }
        public int Size
        {
            get { return this.size; }
        }
        public int MovesPlayedCount
        {
            get { return this.movesPlayedCount; }
        }
        public Cell[,] GetCells()
        {
            return this.cells;
        }
        public Cell GetCellAt(int x, int y)
        {
            return this.cells[x, y];
        }
        public Cell GetCellAt(Location loc)
        {
            return this.cells[loc.X, loc.Y];
        }
        public Cell[] GetCellAt(Location[] locations)
        {
            if (locations == null)
            {
                return new Cell[0];
            }

            Cell[] result = new Cell[locations.Length];

            for (int loopIndex = 0; loopIndex < locations.Length; loopIndex++)
            {
                result[loopIndex] = this.GetCellAt(locations[loopIndex]);
            }

            return result;
        }
        public Cell[][] GetCellsAt(Location[][] locs)
        {
            Cell[][] result = new Cell[locs.GetLength(0)][];

            for (int loopIndex = 0; loopIndex < locs.GetLength(0); loopIndex++)
            {
                result[loopIndex] = this.GetCellAt(locs[loopIndex]);
            }

            return result;
        }
        public Cell[] Row(bool playerX, bool start)
        {
            Cell[] result = new Cell[this.Size];

            // first or last row/col
            int col = start ? 0 : this.Size - 1;

            for (int loopIndex = 0; loopIndex < this.Size; loopIndex++)
            {
                if (playerX)
                {
                    result[loopIndex] = this.GetCellAt(loopIndex, col);
                }
                else
                {
                    result[loopIndex] = this.GetCellAt(col, loopIndex);
                }
            }

            return result;
        }
        public IEnumerable<Location> EmptyCells()
        {
            foreach (Cell cell in this.cells)
            {
                if (cell.IsEmpty())
                {
                    yield return cell.Location;
                }
            }
        }
        public void CopyStateFrom(HexBoard otherBoard)
        {
            if (otherBoard != null)
            {
                if (otherBoard.Size != this.Size)
                {
                    throw new Exception("Board sizes do not match");
                }

                foreach (Cell cell in this.cells)
                {
                    cell.IsOccupied = otherBoard.GetCellAt(cell.Location).IsOccupied;
                }

                this.movesPlayedCount = otherBoard.MovesPlayedCount;
            }
        }
        public Cell[] Neighbours(Cell cell)
        {
            return this.Neighbours(cell.Location);
        }

        public Cell[] Neighbours(Location loc)
        {
            return this.neighbours[loc.X, loc.Y];
        }

        public Cell[][] Neighbours2(Cell cell)
        {
            return this.Neighbours2(cell.Location);
        }

        public Cell[][] Neighbours2(Location loc)
        {
            return this.neighbours2[loc.X, loc.Y];
        }


        public Cell[] BetweenEdge(Location loc, bool playerX)
        {
            if (playerX)
            {
                return this.playerXBetweenEdge[loc.X, loc.Y];
            }

            return this.playerYBetweenEdge[loc.X, loc.Y];
        }
        public void PlayMove(int x, int y, bool playerX)
        {
            this.PlayMove(new Location(x, y), playerX);
        }
        public void PlayMove(Location location, bool playerX)
        {
            Cell cell = this.GetCellAt(location);
            if (!cell.IsEmpty())
            {
                throw new Exception("Cell played is not empty at " + cell);
            }

            cell.IsOccupied = playerX ? Occupied.PlayerX : Occupied.PlayerY;

            this.movesPlayedCount++;
        }
        private void InitCells(int newSize)
        {
            this.size = newSize;
            this.movesPlayedCount = 0;
            this.cells = new Cell[this.Size, this.Size];

            for (int x = 0; x < this.size; x++)
            {
                for (int y = 0; y < this.size; y++)
                {
                    // a new location at x, y
                    Location loc = new Location(x, y);
                    this.cells[x, y] = new Cell(loc);
                }
            }

            this.InitNeighbours();
        }
        private void InitNeighbours()
        {
            HexBoardNeighbours neighboursCalc = new HexBoardNeighbours(this.Size);

            // init the cached neighbours arrays
            this.neighbours = new Cell[this.Size, this.Size][];
            this.neighbours2 = new Cell[this.Size, this.Size][][];
            this.playerXBetweenEdge = new Cell[this.Size, this.Size][];
            this.playerYBetweenEdge = new Cell[this.Size, this.Size][];

            foreach (Cell cell in this.cells)
            {
                Location[] neigbourLocations = neighboursCalc.Neighbours(cell.Location);
                this.neighbours[cell.X, cell.Y] = this.GetCellAt(neigbourLocations);

                Location[][] neighbour2Locations = neighboursCalc.Neighbours2(cell.Location);
                this.neighbours2[cell.X, cell.Y] = this.GetCellsAt(neighbour2Locations);

                Location[] localPlayerXBetweenEdge = neighboursCalc.BetweenEdge(cell.Location, true);
                this.playerXBetweenEdge[cell.X, cell.Y] = this.GetCellAt(localPlayerXBetweenEdge);

                Location[] localPlayerYBetweenEdge = neighboursCalc.BetweenEdge(cell.Location, false);
                this.playerYBetweenEdge[cell.X, cell.Y] = this.GetCellAt(localPlayerYBetweenEdge);
            }
        }
    }
}
