using Othello.Application;
using Othello.Domain.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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
            OthelloAppService.BoardDrawEvent += (_, e) => DrawBoard(e.Placement);
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(BoardGrid);
            var X = (int)(position.X / (BoardGrid.Width + 1) * 8);
            var Y = (int)(position.Y / (BoardGrid.Height + 1) * 8);
            BoardClickEvent(this, new BoardClickEventArgs(new Position(X, Y)));
        }

        private void DrawBoard(Placement placement)
        {
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    if (placement.BlackBoard[x, y])
                    {
                        DrawStone(x, y, Brushes.Black);
                    }
                    if (placement.WhiteBoard[x, y])
                    {
                        DrawStone(x, y, Brushes.White);
                    }
                }
            }
        }

        private void DrawStone(int x, int y, SolidColorBrush brush)
        {
            var ellipse = new Ellipse { Fill = brush, Margin = new Thickness(3) };
            Grid.SetColumn(ellipse, x);
            Grid.SetRow(ellipse, y);
            BoardGrid.Children.Add(ellipse);
        }
    }
}