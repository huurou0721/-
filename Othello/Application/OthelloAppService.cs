using Othello.Domain.Model;
using Othello.Infrastructure;
using Prism.Events;

namespace Othello.Application
{
    public class OthelloAppService
    {
        private readonly IEventAggregator ea_;
        private readonly IBoard board_;

        private readonly Game game_;

        public OthelloAppService(IEventAggregator ea)
        {
            ea_ = ea;
            board_ = new BitBoard();
            game_ = new Game(board_);
        }

        public void TryPut(Position position)
        {
            game_.TryPut(position);
        }
    }
}