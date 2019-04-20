using Hex.Board;
using Hex.Engine.Lookahead;
using Hex.Engine.PathLength;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Engine
{
    public class HexGame
    {
        // the main board

        private readonly HexBoard board;
        private readonly PathLengthBase xPathLength;
        private readonly PathLengthBase yPathLength;
        private readonly GoodMoves goodMoves;

        private bool currentPlayerX = true;
        private int countCellsPlayed;
        private Occupied winner;

        public HexGame(int boardSize)
        {
            this.board = new HexBoard(boardSize);
            IPathLengthFactory pathLengthFactory = new PathLengthAStarFactory();

            this.xPathLength = pathLengthFactory.CreatePathLength(this.board);
            this.yPathLength = pathLengthFactory.CreatePathLength(this.board);
            this.goodMoves = new GoodMoves();
            this.goodMoves.DefaultGoodMoves(boardSize, 5);
        }

        public event EventHandler OnCellPlayed;

        public HexBoard Board
        {
            get { return this.board; }
        }

        public bool PlayerX
        {
            get { return this.currentPlayerX; }
        }

        public int CountCellsPlayed
        {
            get { return this.countCellsPlayed; }
        }

        public Occupied Winner
        {
            get { return this.winner; }
        }

        public GoodMoves GoodMoves
        {
            get { return this.goodMoves; }
        }

        public int SituationScore()
        {
            int playerXScore = this.xPathLength.PlayerScore(true);

            if (playerXScore == 0)
            {
                this.winner = Occupied.PlayerX;
                return this.board.Size;
            }

            if (playerXScore == PathLengthConstants.OffPath)
            {
                this.winner = Occupied.PlayerY;
                return -this.board.Size;
            }
            // compare the path lengths to see who is ahead
            int playerYScore = this.yPathLength.PlayerScore(false);
            this.winner = Occupied.Empty;

            return playerYScore - playerXScore;
        }

        public Occupied HasWon()
        {
            this.SituationScore();

            return this.winner;
        }

        public string DescribeHasWon()
        {
            switch (this.HasWon())
            {
                case Occupied.Empty:
                    return "No winner";

                case Occupied.PlayerX:
                    return "Player X wins";

                case Occupied.PlayerY:
                    return "Player Y wins";

                default:
                    return "Bad has-won value";
            }
        }


        public List<Location> GetCleanPath(bool playerX)
        {
            if (playerX)
            {
                return this.xPathLength.GetCleanPath(true);
            }

            return this.yPathLength.GetCleanPath(false);
        }

        public List<Location> GetCleanPathIntersection()
        {
            List<Location> xPath = this.GetCleanPath(true);
            List<Location> yPath = this.GetCleanPath(false);

            return xPath.Where(yPath.Contains).ToList();
        }



        public void Play(int x, int y)
        {
            // play the cell
            this.board.PlayMove(x, y, this.currentPlayerX);

            // update stats
            this.currentPlayerX = !this.currentPlayerX;
            this.countCellsPlayed++;

            this.goodMoves.MoveUpRows();

            // notify
            if (this.OnCellPlayed != null)
            {
                this.OnCellPlayed(this, EventArgs.Empty);
            }
        }
    }
       
}
