using Othello.Application;
using OthelloClient.Views;
using Prism.Events;
using Prism.Mvvm;

namespace OthelloClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region binding property

        private string title_ = "Othello";

        public string Title
        {
            get => title_;
            set => SetProperty(ref title_, value);
        }

        private string position_;

        public string Position
        {
            get => position_;
            set => SetProperty(ref position_, value);
        }

        #endregion binding property

        private readonly OthelloAppService appService_;

        public MainWindowViewModel(IEventAggregator ea)
        {
            appService_ = new OthelloAppService(ea);
            MainWindow.BoardClickEvent += (_, e) => Position = $"X: {e.Position.X}, Y: {e.Position.Y}";
            MainWindow.BoardClickEvent += (_, e) => appService_.PutTurn(e.Position);
        }
    }
}