using Othello.Domain.Model;
using System;

namespace Othello.Infrastructure.AI
{
    public class RandomMoveAI : IAI
    {
        private Random Random { get; } = new Random();

        public Position DecideMove(Teban teban, IBoard board)
        {
            var legalList = board.GetLegalPosistionList(teban);
            return legalList.Count != 0 ?
                legalList[Random.Next(legalList.Count)] :
                throw (new ArgumentException("合法手が存在しません", nameof(legalList)));
        }
    }
}