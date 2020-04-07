using Othello.Domain.Model;
using Othello.Infrastructure;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Application
{
    public class OthelloAppService
    {
        private readonly IBoard board_;

        public OthelloAppService(IEventAggregator ea)
        {
            ea.GetEvent<PutStoneEvent>().Subscribe(OnStonePut);
        }

        private void OnStonePut(Position position)
        {
            throw new NotImplementedException();
        }

        public bool TryPut(Teban teban, Position position, out IBoard board)
        {
            if (board_.IsLegal(teban, position))
            {
                board = board_.PutTurn(teban, position);
                return true;
            }
            else
            {
                board = null;
                return false;
            }
        }
    }
}
