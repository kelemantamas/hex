using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame
{
    class HexBoardNeighbours
    {
        public int boardSize { get; set; }
        public HexBoardNeighbours(int boardSize)
        {
            this.boardSize = boardSize;
        }

        public bool IsOnBoard(Location loc)
        {
            return (loc.x >= 0) && (loc.y >= 0) &&
                (loc.x < this.boardSize) && (loc.y < this.boardSize);
        }

        public Location[] Neighbours(Location location)
        {
            List<Location> result = new List<Location>();

            if (this.IsOnBoard(location))
            {
                int x = location.x;
                int y = location.y;

                result.Add(new Location(x - 1, y));
                result.Add(new Location(x, y - 1));
                result.Add(new Location(x + 1, y));
                result.Add(new Location(x, y + 1));

                // the above and below neighbours
                result.Add(new Location(x - 1, y + 1));
                result.Add(new Location(x + 1, y - 1));
            }

            // remove locations off the edge of the board
            for (int loopCounter = result.Count - 1; loopCounter >= 0; loopCounter--)
            {
                if (!this.IsOnBoard(result[loopCounter]))
                {
                    result.RemoveAt(loopCounter);
                }
            }

            return result.ToArray();
        }

        public Location[] BetweenEdge(Location loc, bool playerX)
        {
            if (playerX)
            {
                if ((loc.x == 1) && (loc.x < (this.boardSize - 1)))
                {
                    Location[] result = new Location[2];
                    result[0] = new Location(loc.x, 0);
                    result[1] = new Location(loc.x + 1, 0);
                    return result;
                }

                if ((loc.y == this.boardSize - 2) && (loc.x > 0))
                {
                    Location[] result = new Location[2];
                    result[0] = new Location(loc.x - 1, this.boardSize - 1);
                    result[1] = new Location(loc.x, this.boardSize - 1);
                    return result;
                }

                return new Location[0];
            }

            if ((loc.x == 1) && (loc.y < (this.boardSize - 1)))
            {
                Location[] result = new Location[2];
                result[0] = new Location(0, loc.y);
                result[1] = new Location(0, loc.y + 1);
                return result;
            }

            if ((loc.x == this.boardSize - 2) && (loc.y > 0))
            {
                Location[] result = new Location[2];
                result[0] = new Location(this.boardSize - 1, loc.y - 1);
                result[1] = new Location(this.boardSize - 1, loc.y);
                return result;
            }

            return new Location[0];
        }

        public Location[][] Neighbours2(Location location)
        {
            List<Location[]> result = new List<Location[]>();
            int x = location.x;
            int y = location.y;

            if (this.IsOnBoard(location))
            {
                AddTriple(result,
                    x + 1, y + 1,
                    x + 1, y,
                    x, y + 1);

                AddTriple(result,
                    x + 2, y - 1,
                    x + 1, y,
                    x + 1, y - 1);


                AddTriple(result,
                    x + 1, y - 2,
                    x + 1, y - 1,
                    x, y - 1);

                AddTriple(result,
                    x - 1, y - 1,
                    x, y - 1,
                    x - 1, y);

                AddTriple(result,
                    x - 2, y + 1,
                    x - 1, y,
                    x - 1, y + 1);

                AddTriple(result,
                    x - 1, y + 2,
                    x - 1, y + 1,
                    x, y + 1);
            }

            // remove locations off the edge of the board
            for (int loopCounter = result.Count - 1; loopCounter >= 0; loopCounter--)
            {
                if (!this.IsOnBoard(result[loopCounter][0]) ||
                    !this.IsOnBoard(result[loopCounter][1]) ||
                    !this.IsOnBoard(result[loopCounter][2]))
                {
                    result.RemoveAt(loopCounter);
                }
            }

            return result.ToArray();
        }
        private void AddTriple(List<Location[]> result, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            Location[] triple = new Location[3];

            triple[0] = new Location(x1, y1);
            triple[1] = new Location(x2, y2);
            triple[2] = new Location(x3, y3);

            result.Add(triple);
        }
    }
}
