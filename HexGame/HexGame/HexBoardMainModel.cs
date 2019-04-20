using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGame
{
    public class HexBoardMainModel
    {
        private bool CanPlay;
        private int BoardSize;
        private HexGame hexGame;
        public HexBoardMainModel()
        {
            this.CanPlay = true;
            this.BoardSize = 7;
            this.hexGame = new HexGame(this.BoardSize);

            this.Populate();
            this.gameSummary = new GameSummary(this.hexGame, gameData.GameType);
            this.computerSkillLevel = SkillLevelToDepth(gameData.SkillLevel);
            this.doComputerMoveCommand = new DoComputerMoveCommand(this.computerSkillLevel);
            this.CheckComputerMove();
        }
        public void Populate()
        {
            for (int x = 0; x < this.BoardSize; x++)
            {
                for (int y = 0; y < this.BoardSize; y++)
                {
                    HexCellViewModel hexCell = new HexCellViewModel(this)
                    {
                        BoardX = x,
                        BoardY = y
                    };

                    this.Cells.Add(hexCell);
                }
            }
        }

    }
}
