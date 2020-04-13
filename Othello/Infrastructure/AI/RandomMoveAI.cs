using Othello.Domain.Model;
using System;

namespace Othello.Infrastructure.AI
{
    public class RandomMoveAI : IAI
    {
        public Position DecideMove(Teban teban, IBoard board)
        {
            return AIUtility.DecidePositionRandom(teban, board);
        }
    }
}