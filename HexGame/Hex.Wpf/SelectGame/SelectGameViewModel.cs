
namespace Hex.Wpf.SelectGame
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Hex.Wpf.Helpers;
    using Hex.Wpf.Model;

    public class SelectGameViewModel : BaseViewModel
    {
        

        private readonly ActionCommand<SelectGameViewModel> successCommand;
        private readonly ActionCommand<SelectGameViewModel> cancelCommand;

        private int selectedBoardSize = 7;
        private GameType gameType;

        public SelectGameViewModel(Action<SelectGameViewModel> successAction, Action<SelectGameViewModel> cancelAction)
        {
            this.successCommand = new ActionCommand<SelectGameViewModel>(successAction);
            this.cancelCommand = new ActionCommand<SelectGameViewModel>(cancelAction);
            this.SelectedBoardSize = 7;
            this.HumanVersusComputer = true;
            this.SkillLevel = ComputerSkillLevel.Medium;
        }

        public int SelectedBoardSize { get; set; }

      

        public GameType GameType
        {
            get
            {
                return this.gameType;
            }

            set
            {
                if (this.gameType != value)
                {
                    this.gameType = value;
                    this.EnableOk();
                    this.OnPropertyChanged("GameType");
                }
            }
        }

   

        public bool HumanVersusComputer
        {
            get
            {
                return this.gameType == GameType.HumanVersusComputer;
            }

            set
            {
                if (value != this.HumanVersusComputer)
                {
                    this.GameType = value ? GameType.HumanVersusComputer : GameType.Unknown;
                    this.OnPropertyChanged("HumanVersusComputer");
                }
            }
        }









        public bool SkillMediumChecked
        {
            get
            {
                return this.SkillLevel == SelectGame.ComputerSkillLevel.Medium;
            }

            set
            {
                if (value != this.SkillMediumChecked)
                {
                    this.SkillLevel = value ? ComputerSkillLevel.Medium : ComputerSkillLevel.Unknown;
                    this.OnPropertyChanged("SkillMediumChecked");
                }
            }
        }

        public ComputerSkillLevel SkillLevel { get; set; }

        public ICommand SuccessCommand
        {
            get { return this.successCommand; }
        }

        public ICommand CancelCommand
        {
            get { return this.cancelCommand; }
        }

        private void EnableOk()
        {
            this.successCommand.Enabled = this.IsOk();
        }

        private bool IsOk()
        {
            return this.selectedBoardSize > 0 && this.gameType != GameType.Unknown;
        }
    }
}
