using System;

namespace Othello.Domain.Model
{
    public class Game
    {
        private IBoard board_;

        private Teban teban_;

        public static event EventHandler<BoardDrawEventArgs> BoardDrawEvent;

        public Game(IBoard board)
        {
            board_ = board;
        }

        public void TryPut(Position position)
        {
            if (board_.IsLegal(teban_, position))
            {
                board_ = board_.PutTurn(teban_, position);
            }
        }

        private void DrawBoard(Placement placement)
        {
            BoardDrawEvent(this, new BoardDrawEventArgs(placement));
        }

        private void SwitchTeban()
        {
            switch (teban_)
            {
                case Teban.Black:
                    teban_ = Teban.White;
                    break;

                case Teban.White:
                    teban_ = Teban.Black;
                    break;

                default:
                    break;
            }
        }
    }
}