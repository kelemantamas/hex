
namespace Hex.Wpf
{
    using System.Windows.Input;

    using Hex.Wpf.Controls;
    using Hex.Wpf.Helpers;

    public class MainWindowViewModel : BaseViewModel
    {
        private readonly HexBoardViewModel hexBoard;
 
   

        public MainWindowViewModel(HexBoardViewModel hexBoard)
        {
            this.hexBoard = hexBoard;
            this.hexBoard.OnCellPlayed += this.HexBoardCellPlayed;

        }

        public HexBoardViewModel HexBoard
        {
            get { return this.hexBoard;  }
        }

        private void HexBoardCellPlayed()
        {
        }

        private void DoComputerMove()
        {
            this.HexBoard.DoComputerMoveCommand.Execute(this.HexBoard);
        }
    }
}
