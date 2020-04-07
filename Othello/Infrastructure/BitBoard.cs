using Othello.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Infrastructure
{
    class BitBoard : IBoard
    {
        public Placement Placement
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public (int black, int white, int total) CountStone()
        {
            throw new NotImplementedException();
        }

        public bool IsLegal(Teban teban, Position position)
        {
            throw new NotImplementedException();
        }

        public List<Position> LegalList(Teban teban, Position position)
        {
            throw new NotImplementedException();
        }

        public IBoard PutTurn(Teban teban, Position position)
        {
            throw new NotImplementedException();
        }
    }
}
