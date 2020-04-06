using OthelloClient.Views;
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

        public MainWindowViewModel()
        {
            MainWindow.BoardClickEvent += (_, e) => Position = $"X: {e.X}, Y: {e.Y}";
        }
    }
}