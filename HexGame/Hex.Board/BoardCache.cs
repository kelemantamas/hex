using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Board
{
    public class BoardCache
    {
        private readonly List<HexBoard> available = new List<HexBoard>();
        private readonly List<HexBoard> inUse = new List<HexBoard>();
        private readonly int boardSize;
        private readonly object locker = new object();

        public BoardCache(int boardSize)
        {
            this.boardSize = boardSize;
        }

        public int BoardSize
        {
            get { return this.boardSize; }
        }

        public HexBoard GetBoard()
        {
            lock (this.locker)
            {
                HexBoard result;
                if (this.available.Count == 0)
                {
                    // no boards available, so make a new one
                    result = new HexBoard(this.BoardSize);
                }
                else
                {
                    // return any available board - less jiggering to take the end one
                    int boardIndex = this.available.Count - 1;
                    result = this.available[boardIndex];
                    this.available.RemoveAt(boardIndex);

                    if (this.available.Count == 0)
                    {
                        Task.Factory.StartNew(() => this.available.Add(new HexBoard(this.BoardSize)));
                    }
                }

                // board is now in use
                this.inUse.Add(result);
                return result;
            }
        }

        public void Release(HexBoard board)
        {
            lock (this.locker)
            {
                this.inUse.Remove(board);
                this.available.Add(board);
            }
        }

    }
}
