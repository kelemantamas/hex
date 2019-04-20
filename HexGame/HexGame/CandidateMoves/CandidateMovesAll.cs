using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame.CandidateMoves
{
    public class CandidateMovesAll:ICandidateMoves
    {
        public IEnumerable<Location> CandidateMoves(HexBoard board, int lookaheadDepth)
        {
            return board.EmptyCells();
        }
    }
}
