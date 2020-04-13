using Othello.Domain.Model;
using System;

namespace Othello.Infrastructure.AI
{
    public static class AIUtility
    {
        private static Random Random { get; } = new Random();

        public static Position DecidePositionRandom(Teban teban, IBoard board)
        {
            var legalList = board.GetLegalPosistionList(teban);
            return legalList.Count != 0
                ? legalList[Random.Next(legalList.Count)]
                : throw (new ArgumentException("合法手が存在しません", nameof(legalList)));
        }
    }
}
}