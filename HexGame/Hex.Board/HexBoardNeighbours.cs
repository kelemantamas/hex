using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Board
{
    public class HexBoardNeighbours
    {
        private readonly int boardSize;
        public HexBoardNeighbours(int boardSize)
        {
            this.boardSize = boardSize;
        }

        public int BoardSize
        {
            get { return this.boardSize; }
        }

        public bool IsOnBoard(Location loc)
        {
            return (loc.X >= 0) && (loc.Y >= 0) &&
                (loc.X < this.BoardSize) && (loc.Y < this.BoardSize);
        }

        public Location[] Neighbours(Location location)
        {
            List<Location> result = new List<Location>();

            if (this.IsOnBoard(location))
            {
                int x = location.X;
                int y = location.Y;

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

        public Location[][] Neighbours2(Location location)
        {
            List<Location[]> result = new List<Location[]>();
            int x = location.X;
            int y = location.Y;

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

        public Location[] BetweenEdge(Location loc, bool playerX)
        {
            if (playerX)
            {
                if ((loc.Y == 1) && (loc.X < (this.BoardSize - 1)))
                {
                    Location[] result = new Location[2];
                    result[0] = new Location(loc.X, 0);
                    result[1] = new Location(loc.X + 1, 0);
                    return result;
                }

                if ((loc.Y == this.BoardSize - 2) && (loc.X > 0))
                {
                    Location[] result = new Location[2];
                    result[0] = new Location(loc.X - 1, this.BoardSize - 1);
                    result[1] = new Location(loc.X, this.BoardSize - 1);
                    return result;
                }

                return new Location[0];
            }

            if ((loc.X == 1) && (loc.Y < (this.BoardSize - 1)))
            {
                Location[] result = new Location[2];
                result[0] = new Location(0, loc.Y);
                result[1] = new Location(0, loc.Y + 1);
                return result;
            }

            if ((loc.X == this.BoardSize - 2) && (loc.Y > 0))
            {
                Location[] result = new Location[2];
                result[0] = new Location(this.BoardSize - 1, loc.Y - 1);
                result[1] = new Location(this.BoardSize - 1, loc.Y);
                return result;
            }

            return new Location[0];
        }

        private static void AddTriple(List<Location[]> result, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            Location[] triple = new Location[3];

            triple[0] = new Location(x1, y1);
            triple[1] = new Location(x2, y2);
            triple[2] = new Location(x3, y3);

            result.Add(triple);
        }
    }
}
