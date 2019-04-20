using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame
{
    public enum Occupied
    {
        Empty=0,
        PlayerX,
        PlayerY
    }
    public class HexBoard
    {
        private Cell[,] cells;
        private Cell[,][] neighbours;
        private Cell[,][][] neighbours2;
        private Cell[,][] playerX;
        private Cell[,][] playerY;
        public int Size { get; set; }
        public int movesPlayedCount { get; set; }
        public HexBoard(int size)
        {
            InitCells(size);
        }

        public HexBoard (HexBoard originalBoard)
        {
            if(originalBoard != null)
            {
                this.InitCells(originalBoard.Size);
                this.CopyStateFrom(originalBoard);
            }
        }
        // implementalni a Location-t
        
        private void InitCells(int newSize)
        {
            Size = newSize;
            movesPlayedCount = 0;
            cells = new Cell[Size, Size];

            for (int x=0; x<Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    // uj helyet keresek x, y-nak

                    Location loc = new Location(x, y);
                    this.cells[x, y] = new Cell(loc);

                }
            }

            InitNeighbours();
        }

        public Cell GetCellAt(Location loc)
        {
            return this.cells[loc.x, loc.y];
        }

        public void CopyStateFrom (HexBoard otherBoard)
        {
            if (otherBoard != null)
            {
                if (otherBoard.Size != this.Size)
                {
                    throw new Exception("Board size error");
                }

                foreach (var cell in this.cells)
                {
                    cell.IsOccupied = otherBoard.GetCellAt(cell.Location).IsOccupied;
                }

                this.movesPlayedCount = otherBoard.movesPlayedCount;
            }
        }
        private void InitNeighbours()
        {
            HexBoardNeighbours neighboursCalc = new HexBoardNeighbours(Size);
            this.neighbours = new Cell[Size, Size][];
            this.neighbours2 = new Cell[this.Size, this.Size][][];
            this.playerX = new Cell[Size, Size][];
            this.playerY = new Cell[Size, Size][];

            foreach (Cell cell in this.cells)
            {
                Location[] neigbourLocations = neighboursCalc.Neighbours(cell.Location);
                this.neighbours[cell.x, cell.y] = this.GetCellAt(neigbourLocations);

                Location[][] neighbour2Locations = neighboursCalc.Neighbours2(cell.Location);
                this.neighbours2[cell.x, cell.y] = this.GetCellsAt(neighbour2Locations);

                Location[] localPlayerXBetweenEdge = neighboursCalc.BetweenEdge(cell.Location, true);
                this.playerX[cell.x, cell.y] = this.GetCellAt(localPlayerXBetweenEdge);

                Location[] localPlayerYBetweenEdge = neighboursCalc.BetweenEdge(cell.Location, false);
                this.playerY[cell.x, cell.y] = this.GetCellAt(localPlayerYBetweenEdge);
            }

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

        public Cell[,] GetCells()
        {
            return this.cells;
        }

        public Cell[] Neighbours(Location loc)
        {
            return this.neighbours[loc.x, loc.y];
        }

        public Cell[][] Neighbours2(Cell cell)
        {
            return this.Neighbours2(cell.Location);
        }
        public Cell[][] Neighbours2(Location loc)
        {
            return this.neighbours2[loc.X, loc.Y];
        }

        public Cell[] Neighbours(Cell cell)
        {
            return this.Neighbours(cell.Location);
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

    }
}
