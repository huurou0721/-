using Othello.Application;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace OthelloClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region binding property

        private string title_ = "Othello";
        private List<string> aiList_;
        private string selectedBlackAI_;
        private string selectedWhiteAI_;

        public string Title
        {
            get => title_;
            set => SetProperty(ref title_, value);
        }

        public List<string> AIList
        {
            get => aiList_;
            set => SetProperty(ref aiList_, value);
        }

        public string SelectedBlackAI
        {
            get => selectedBlackAI_;
            set => SetProperty(ref selectedBlackAI_, value);
        }

        public string SelectedWhiteAI
        {
            get => selectedWhiteAI_;
            set => SetProperty(ref selectedWhiteAI_, value);
        }

        private string resultText_;

        public string ResultText
        {
            get => resultText_;
            set => SetProperty(ref resultText_, value);
        }

        #endregion binding property

        #region binding command

        private DelegateCommand playCmd_;

        public DelegateCommand PlayCmd =>
            playCmd_ ?? (playCmd_ = new DelegateCommand(ExecutePlayCmd, CanExecutePlayCmd)
                .ObservesProperty(() => SelectedBlackAI)
                .ObservesProperty(() => SelectedWhiteAI));

        #endregion binding command

        public static event EventHandler BoardInitializeEvent;

        private readonly IEventAggregator ea_;

        private OthelloAppService appService_;

        public MainWindowViewModel(IEventAggregator ea)
        {
            ea_ = ea;

            AIList = new List<string>
            {
                "RandomMoveAI",
                "MonteCarloAI",
            };

            appService_ = new OthelloAppService(ea, SelectedBlackAI, SelectedWhiteAI);
        }

        private async void ExecutePlayCmd()
        {
            BoardInitializeEvent(this, null);
            appService_ = new OthelloAppService(ea_, SelectedBlackAI, SelectedWhiteAI);
            await appService_.Run();
            ResultText = appService_.End();
        }

        private bool CanExecutePlayCmd()
        {
            return !string.IsNullOrEmpty(SelectedBlackAI) && !string.IsNullOrEmpty(SelectedWhiteAI);
        }
    }
}