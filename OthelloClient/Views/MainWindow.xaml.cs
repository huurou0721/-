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
            Game.BoardDrawEvent += DrawBoard;
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(BoardGrid);
            var X = (int)(position.X / (BoardGrid.Width + 1) * 8);
            var Y = (int)(position.Y / (BoardGrid.Height + 1) * 8);
            BoardClickEvent(this, new BoardClickEventArgs(new Position(X, Y)));

            var placement = new Placement(
                new bool[8, 8] { { false, false, false, false, false, false, false, false },
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, true, false, false, false, },
                                 { false, false, false, true, false, false, false, false, },
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,}
                },
                new bool[8, 8] { { false, false, false, false, false, false, false, false },
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, true, false, false, false, false, },
                                 { false, false, false, false, true, false, false, false, },
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,}
                }
                );
        }

        private void DrawBoard(object sender, BoardDrawEventArgs e)
        {
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    if (e.Placement.BlackBoard[x, y]) DrawBlackStone(x, y);
                    if (e.Placement.WhiteBoard[x, y]) DrawWhiteStone(x, y);
                }
            }
        }

        private void DrawBlackStone(int x, int y)
        {
            var ellipse = new Ellipse { Fill = Brushes.Black, Margin = new Thickness(3) };
            Grid.SetColumn(ellipse, x);
            Grid.SetRow(ellipse, y);
            BoardGrid.Children.Add(ellipse);
        }

        private void DrawWhiteStone(int x, int y)
        {
            var ellipse = new Ellipse { Fill = Brushes.White, Margin = new Thickness(3) };
            Grid.SetColumn(ellipse, x);
            Grid.SetRow(ellipse, y);
            BoardGrid.Children.Add(ellipse);
        }
    }
}