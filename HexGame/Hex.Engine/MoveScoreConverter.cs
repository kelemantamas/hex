using Hex.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Engine
{
    public static class MoveScoreConverter
    {
        private const int WinScore = 10000;
        private const int ScoreMultiplier = 100;
        private const int MaxDepth = 20;

        public static int ConvertWin(Occupied winner, int depth)
        {
            int result = (WinScore + MaxDepth) - depth;
            return NegateForPlayerY(result, winner);
        }



        public static bool IsWin(int score)
        {
            return Math.Abs(score) >= WinScore;
        }

        public static Occupied Winner(int score)
        {
            if (IsWinForPlayer(score, Occupied.PlayerX))
            {
                return Occupied.PlayerX;
            }

            if (IsWinForPlayer(score, Occupied.PlayerY))
            {
                return Occupied.PlayerY;
            }

            return Occupied.Empty;
        }

        public static bool IsWinForPlayer(int score, Occupied player)
        {
            int positveScore = NegateForPlayerY(score, player);
            return positveScore >= WinScore;
        }

        public static bool IsBetterFor(int score1, int score2, bool playerX)
        {
            if (playerX)
            {
                return score1 > score2;
            }

            return score1 < score2;
        }


        public static int WinDepth(int score)
        {
            return (WinScore + MaxDepth) - Math.Abs(score);
        }


        private static int NegateForPlayerY(int score, Occupied player)
        {
            if (player == Occupied.PlayerY)
            {
                return score * -1;
            }

            return score;
        }
    }
}
