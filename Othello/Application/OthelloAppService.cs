using Othello.Domain.Model;
using Othello.Infrastructure;
using Othello.Infrastructure.AI;
using Prism.Events;
using System;

namespace Othello.Application
{
    public class OthelloAppService
    {
        public static event EventHandler<BoardDrawEventArgs> BoardDrawEvent;

        private readonly IEventAggregator ea_;
        private IBoard board_;
        private readonly IAI ai_;

        private Teban teban_;

        public OthelloAppService(IEventAggregator ea)
        {
            ea_ = ea;
            board_ = new BitBoard(34628173824, 68853694464);
            ai_ = new RandomMoveAI();
        }

        public void PutTurn(Position position)
        {
            if (board_.IsLegal(teban_, position))
            {
                board_ = board_.PutTurn(teban_, position);
                SwitchTeban();
            }
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

        private void DrawBoard(Placement placement)
        {
            BoardDrawEvent(this, new BoardDrawEventArgs(placement));
        }
    }
}