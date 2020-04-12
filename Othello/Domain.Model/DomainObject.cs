using System;

namespace Othello.Domain.Model
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Placement
    {
        /// <summary>
        /// bool[X,Y]
        /// </summary>
        public bool[,] BlackBoard { get; }

        /// <summary>
        /// bool[X,Y]
        /// </summary>
        public bool[,] WhiteBoard { get; }

        public Placement(bool[,] blackBoard, bool[,] whiteBoard)
        {
            if (!(blackBoard.GetLength(0) == 8 && blackBoard.GetLength(1) == 8))
                throw new ArgumentException("Black board is illegal size.");
            if (!(whiteBoard.GetLength(0) == 8 && whiteBoard.GetLength(1) == 8))
                throw new ArgumentException("White board is illegal size.");

            BlackBoard = blackBoard;
            WhiteBoard = whiteBoard;
        }
    }

    public enum Teban
    {
        Black = 0,
        White = 1,
    }
}