using System;

namespace Othello.Domain.Model
{
    public class BoardClickEventArgs : EventArgs
    {
        public Position Position { get; set; }

        public BoardClickEventArgs(Position position)
        {
            Position = position;
        }
    }

    public class BoardDrawEventArgs : EventArgs
    {
        public Placement Placement { get; }

        public BoardDrawEventArgs(Placement placement)
        {
            Placement = placement;
        }
    }
}