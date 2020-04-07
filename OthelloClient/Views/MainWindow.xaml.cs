using Othello.Domain.Model;
using System;
using System.Windows;

namespace OthelloClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static event EventHandler<BoardClickEventArgs> BoardClickEvent;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BoardGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.GetPosition(BoardGrid);
            var X = (int)(position.X / (BoardGrid.Width + 1) * 8);
            var Y = (int)(position.Y / (BoardGrid.Height + 1) * 8);
            BoardClickEvent(this, new BoardClickEventArgs(new Position(X, Y)));
        }
    }

    public class BoardClickEventArgs : EventArgs
    {
        public Position Position { get; set; }

        public BoardClickEventArgs(Position position)
        {
            Position = position;
        }
    }
}