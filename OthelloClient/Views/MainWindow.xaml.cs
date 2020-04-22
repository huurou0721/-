using Othello.Application;
using Othello.Domain.Model;
using OthelloClient.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OthelloClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            OthelloAppService.BoardDrawEvent += (_, e) => DrawBoard(e.Placement);
            MainWindowViewModel.BoardInitializeEvent += (_, __) => InitializeBoard();
        }

        private void InitializeBoard()
        {
            BoardGrid.Children.Clear();
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    var border = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(1) };
                    Grid.SetColumn(border, x);
                    Grid.SetRow(border, y);
                    BoardGrid.Children.Add(border);
                }
            }
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