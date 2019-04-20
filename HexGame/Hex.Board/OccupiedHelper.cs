using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Board
{
    public enum Occupied
    {
        Empty = 0,
        PlayerX,
        PlayerY
    }
    public static class OccupiedHelper
    {
        public static Occupied Opponent(this Occupied player)
        {
            switch (player)
            {
                case Occupied.PlayerX:
                    return Occupied.PlayerY;

                case Occupied.PlayerY:
                    return Occupied.PlayerX;

                default:
                    throw new Exception("No opponent for Occupied " + player);
            }
        }



        public static Occupied ToPlayer(this bool isPlayerX)
        {
            return isPlayerX ? Occupied.PlayerX : Occupied.PlayerY;
        }

        public static string OccupiedToString(Occupied occupied)
        {
            switch (occupied)
            {
                case Occupied.Empty:
                    return "-";
                case Occupied.PlayerX:
                    return "X";
                case Occupied.PlayerY:
                    return "Y";
                default:
                    return "?";
            }
        }
    }
}
