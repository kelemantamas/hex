
namespace Hex.Wpf.Controls
{
    using Hex.Wpf.Helpers;
    using Hex.Wpf.Model;

    public class DoComputerMoveCommand : GenericCommand<HexBoardViewModel>
    {
        private HexBoardViewModel currentViewModel;

        public DoComputerMoveCommand()
        {
        }

        public override void ExecuteOnValue(HexBoardViewModel value)
        {
            this.currentViewModel = value;
            ComputerMoveCalculator moveCalculator = new ComputerMoveCalculator(value.Game, this.MoveCompleted);
            moveCalculator.Move();
        }

        private void MoveCompleted(ComputerMoveData computerMoveData)
        {
            HexCellViewModel cellToPlay = this.currentViewModel.GetCellAtLocation(computerMoveData.Move);
            if (cellToPlay != null)
            {
                cellToPlay.PlayCell();
                this.currentViewModel.SetLastMoveDuration(computerMoveData.Time);
            }
        }
    }
}
