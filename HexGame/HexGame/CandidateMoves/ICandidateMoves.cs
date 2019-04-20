using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame.CandidateMoves
{
    public interface ICandidateMoves
    {
       IEnumerable<Location> CandidateMoves(HexBoard board, int lookaheadDepth);
    }
}
