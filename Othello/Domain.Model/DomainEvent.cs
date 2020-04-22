using System;

namespace Othello.Domain.Model
{
    public class BoardDrawEventArgs : EventArgs
    {
        public Placement Placement { get; }

        public BoardDrawEventArgs(Placement placement)
        {
            Placement = placement;
        }
    }
}